using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using SimGame.Data;
using SimGame.Data.Interface;
using SimGame.Website.Models;

namespace SimGame.Website.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Authentication/
        readonly IGameSimContext _gameSimContext = new GameSimContext();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = Encrypt(user.UserName);
                    var password = Encrypt(user.Password);
                    if (UserIsValid(username, password))
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index3", "Manage");
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            catch
            {
                return View(user);
            }
            return View(user);
        }

        private bool UserIsValid(string username, string password)
        {
            return true;
//            return _gameSimContext.Users.Any(x => x.UserName.Equals(username) && x.Password.Equals(password));
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = Encrypt(user.UserName);
                    var password = Encrypt(user.Password);
                    if (_gameSimContext.Users.Any(x=>x.UserName.Equals(username)))
                    {
                        ModelState.AddModelError("", "The username specified already exists");
                        return View(user);
                    }
                    _gameSimContext.Users.Add(new SimGame.Domain.User
                    {
                        Password = password,
                        UserName = username
                    });

                    _gameSimContext.Commit();
                }
            }
            catch
            {
                return View(user);
            }
            return View(user);
        }

        #region Encryption Methods

        private string Encrypt(string clearText)
        {
            var EncryptionKey = "MAKV2SPBNI99212";
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                // ReSharper disable once PossibleNullReferenceException
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        #endregion

    }
}
