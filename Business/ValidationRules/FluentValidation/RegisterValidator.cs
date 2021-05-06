using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator : AbstractValidator<UserForRegisterDto>
    {

        public RegisterValidator()
        {

            //RuleFor(r => r.Password).Must(PasswordRules)
                //.WithMessage("Password must contain special character letters and numbers!");

            //RuleFor(r => r.RecoveryEmail).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
            //RuleFor(r => r.Password).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
           //// RuleFor(r => r.Address1).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
           ///RuleFor(r => r.Address2).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
           // RuleFor(r => r.RecoveryEmail).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
            //RuleFor(r => r.RecoveryEmail).Must(DenyTurkishCharacter).WithMessage("You can not use turkish characters!");
        }

        //Kullanıcının Türkçe Karakter Kullanmasının önüne geçildi.
        private bool DenyTurkishCharacter(string arg)
        {
            string turkishCharacters = "çğıiöüşÇĞİÖÜŞ";

            char[] property = arg.ToCharArray();

            foreach (var prop in property)
            {
                if (prop.CompareTo(turkishCharacters) < 1)
                {
                    return true;
                }
            }

            return false;
        }

        //Güçlü bir parola oluşturmak için kullanıcıyı zorlamaya yaradı.
            private bool PasswordRules(string arg)
            {
                string smallLatters = "abcdefghijklmnoöprstuvyzwx";
                string bigLatters = "ABCÇDEFGHIJKLMNOÖPRSTUVYZWX";
                string specialCharacters = "!£#$&?*-_|";

                var compare = specialCharacters + smallLatters + bigLatters;

                Char[] passwordToControles = arg.ToCharArray();

                bool chose = false;

                foreach (var password in passwordToControles)
                {
                    foreach (var compored in compare)
                    {
                        var controlForNumber = char.IsNumber(compored);

                        if (password.CompareTo(compored) < 1)
                        {
                            chose = false;
                        }

                        if (!chose && !controlForNumber)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
