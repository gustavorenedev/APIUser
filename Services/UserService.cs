using ProjectFor7COMm.Models;
using ProjectFor7COMm.Repositories;
using System.Security.Cryptography;
using System.Text;

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
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<bool> ValidateUser(string username, string password)
        {
            var userList = await _userRepository.GetAll();
            var user = userList.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return false;

            return VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt));
        }

        public async Task RegisterUser(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            await _userRepository.Add(user);
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

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return false;

            await _userRepository.Delete(id);
            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                // Verifica se o usuário existe no banco de dados
                var existingUser = await _userRepository.GetById(user.Id);
                if (existingUser == null)
                    return false; // Se não existir, retorna false indicando falha na atualização

                // Atualiza os campos necessários do usuário existente
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;

                // Chama o método de atualização do repositório
                await _userRepository.Update(existingUser);

                return true; // Retorna true indicando sucesso na atualização
            }
            catch (Exception ex)
            {
                // Manipulação de exceções, registro de logs, etc.
                return false; // Retorna false indicando falha na atualização em caso de exceção
            }
        }

    }
}
