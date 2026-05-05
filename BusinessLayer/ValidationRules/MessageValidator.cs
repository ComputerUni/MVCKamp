using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator:AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Mail Adresini Boş Geçemezsin");
            RuleFor(x => x.SenderMail).NotEmpty().WithMessage("Gönderici Mail Adresini Boş Geçemezsin");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Alanını Boş Geçemezsin");
            RuleFor(x => x.ReceiverMail).EmailAddress().WithMessage("Alıcı Mail Adresi İçin Geçersiz E-Posta Formatı");
            RuleFor(x => x.SenderMail).EmailAddress().WithMessage("Gönderici Mail Adresi İçin Geçersiz E-Posta Formatı");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen En Az 3 Karakter Girişi Yapın");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen 100 Karakterden Fazla Değer Girişi Yapmayın");

        }
    }
}
