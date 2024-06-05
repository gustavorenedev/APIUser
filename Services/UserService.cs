using ProjectFor7COMm.Models;
using ProjectFor7COMm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFor7COMm.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all users.", ex);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the user with ID {id}.", ex);
            }
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            try
            {
                var userList = await _userRepository.GetAll();
                var user = userList.FirstOrDefault(u => u.Username == username);

                if (user == null)
                    return false;

                return VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while validating the user.", ex);
            }
        }

        public async Task RegisterUser(User user, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmail(user.Email);
                if (existingUser != null)
                {
                    throw new Exception("Email already exists.");
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = Convert.ToBase64String(passwordHash);
                user.PasswordSalt = Convert.ToBase64String(passwordSalt);

                await _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user.", ex);
            }
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<bool> ResetPassword(string email, string newPassword)
        {
            try
            {
                var userList = await _userRepository.GetAll();
                var user = userList.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return false;

                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = Convert.ToBase64String(passwordHash);
                user.PasswordSalt = Convert.ToBase64String(passwordSalt);

                await _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resetting the user's password.", ex);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                    return false;

                await _userRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the user with ID {id}.", ex);
            }
        }

        public async Task<bool> UpdateUser(int id, User user)
        {
            try
            {
                var existingUser = await _userRepository.GetById(id);
                if (existingUser == null)
                    return false;

                var userWithSameEmail = await _userRepository.GetByEmail(user.Email);
                if (userWithSameEmail != null && userWithSameEmail.Id != id)
                {
                    throw new Exception("Email already exists for another user.");
                }

                existingUser.Username = user.Username;
                existingUser.Email = user.Email;

                await _userRepository.Update(existingUser);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the user with ID {id}.", ex);
            }
        }
    }
}
