using System.Linq;

namespace ApplicationContextData
{
    public class AddNameKb
    {
        public void AddNameKbAndPass()
        {
            using ApplicationContext application = new();
            var checkFOrNull = application.NamesKb.Select(p => p.NameKbOgk).FirstOrDefault();
            if (checkFOrNull != null) return;
            NameKB nameKb = new NameKB { NameKbOgk = "КБ КГА", PasswordsKb = "qwjqky" };
            NameKB nameKb1 = new NameKB { NameKbOgk = "КБ КА", PasswordsKb = "sna3mk" };
            NameKB nameKb2 = new NameKB { NameKbOgk = "КБ РА", PasswordsKb = "dhh0uw" };
            NameKB nameKb3 = new NameKB { NameKbOgk = "КБ Блоч. и автом. уст.", PasswordsKb = "7v75iw" };
            NameKB nameKb4 = new NameKB { NameKbOgk = "КБ ТА", PasswordsKb = "97fhfj" };
            NameKB nameKb5 = new NameKB { NameKbOgk = "КБ Отгрузки", PasswordsKb = "es3fv8" };
            NameKB nameKb6 = new NameKB { NameKbOgk = "КБ ЗРА", PasswordsKb = "5vw6q2" };
            NameKB nameKb7 = new NameKB { NameKbOgk = "КБ АПА", PasswordsKb = "zbodpt" };
            NameKB nameKb8 = new NameKB { NameKbOgk = "КБ Спец.ТА", PasswordsKb = "ndnwx3" };
            NameKB nameKb9 = new NameKB { NameKbOgk = "КБ КЗП", PasswordsKb = "bmkmnc" };

            application.NamesKb.Add(nameKb);
            application.NamesKb.Add(nameKb1);
            application.NamesKb.Add(nameKb2);
            application.NamesKb.Add(nameKb3);
            application.NamesKb.Add(nameKb4);
            application.NamesKb.Add(nameKb5);
            application.NamesKb.Add(nameKb6);
            application.NamesKb.Add(nameKb7);
            application.NamesKb.Add(nameKb8);
            application.NamesKb.Add(nameKb9);
            application.SaveChanges();
        }
    }
}
