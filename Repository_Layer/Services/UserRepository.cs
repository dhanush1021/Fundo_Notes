using Common_Layer.RequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aes = System.Security.Cryptography.Aes;

namespace Repository_Layer.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly EntityContext context;
        public UserRepository(EntityContext _Context)
        {
            context = _Context;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity userEntity = new UserEntity();
            Encrypted_Keys encrypted_Keys = new Encrypted_Keys();
            List<byte[]> bytes = new List<byte[]>();
            userEntity.firstname = model.firstname;
            userEntity.lastname = model.lastname;
            userEntity.email = model.email;
            bytes = Encrypt(model.password);
            encrypted_Keys.Key = Convert.ToBase64String(bytes[0]);
            encrypted_Keys.IV = Convert.ToBase64String(bytes[1]);
            userEntity.password = Convert.ToBase64String(bytes[2]);
            context.Keys.Add(encrypted_Keys);
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }
        public async Task<UserEntity> LoginUser(LoginModel model)
        {
            var entity = await context.UserTable.SingleOrDefaultAsync(user => user.email == model.email);
            if(entity == null)
            {
                return null;
            }
            else
            {
                Encrypted_Keys key = await context.Keys.SingleOrDefaultAsync(user => user.UserId == entity.UserId);
                List<byte[]> bytes = new List<byte[]>();
                bytes.Add(Convert.FromBase64String(key.Key));
                bytes.Add(Convert.FromBase64String(key.IV));
                bytes.Add(Convert.FromBase64String(entity.password));
                string passwo = decrypt(bytes);
                if (passwo.Substring(0, passwo.Length - 2) == model.password)
                {
                    return entity;
                }
                else
                {
                    entity.password = "incorrect";
                    return entity;
                }
            }
        }
        public List<byte[]> Encrypt(string password)
        {
            List<byte[]> result = new List<byte[]>();
            using(Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                result.Add(aes.Key);
                result.Add(aes.IV);
                ICryptoTransform Encrypter = aes.CreateEncryptor(aes.Key,aes.IV);
                using(MemoryStream memory = new MemoryStream())
                {
                    using(CryptoStream crypt = new CryptoStream(memory, Encrypter, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(crypt))
                        {
                            sw.WriteLine(password);
                        }
                        result.Add(memory.ToArray());
                    }
                }
            }
            return result;
        }
        public string decrypt(List<byte[]> data)
        {
            string pass;
            using (Aes aes = Aes.Create())
            {
                aes.Key = data[0];
                aes.IV = data[1];
                ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(data[2]))
                {
                    using (CryptoStream Decrypt = new CryptoStream(ms, decrypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(Decrypt))
                        {
                            pass = sr.ReadToEnd();
                        }
                    }
                }
            }
            return pass;
        }
    }
}
