using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seminarska_naloga_10
{
    internal class Program
    {

        public static List<Profil> _profili { get; set; }
        public static List<Filmi> _filmi { get; set; }

        public static List<Uporabik> _uporabniki { get; set; }


        static void Main(string[] args)
        {

            _filmi = NapolniSeznamFilmov();

            _uporabniki = NapolniSeznamUporabnikov();

            _profili = NapolniSeznamProfilov();

            IzpisiFilme(_filmi);

            IzpisiUporabnike(_uporabniki);
            PredlagajNasledniFilm();



        }

        static List<Filmi> NapolniSeznamFilmov()
        {
            var _filmi = new List<Filmi>();
            _filmi.Add(new Filmi() { id = 1, naslov = " Insise out" });
            _filmi.Add(new Filmi() { id = 2, naslov = " Minions" });
            _filmi.Add(new Filmi() { id = 3, naslov = " Avengers" });
            _filmi.Add(new Filmi() { id = 4, naslov = " Ant - Man" });
            return _filmi;
        }

        static List<Uporabik> NapolniSeznamUporabnikov()
        {
            var _uporabniki = new List<Uporabik>();

            _uporabniki.Add(new Uporabik() { id = 1, ime = " Jason" });
            _uporabniki.Add(new Uporabik() { id = 2, ime = " Andi" });
            _uporabniki.Add(new Uporabik() { id = 3, ime = " Sarah" });
            _uporabniki.Add(new Uporabik() { id = 4, ime = " Sam" });
            _uporabniki.Add(new Uporabik() { id = 5, ime = " Scaz" });

            return _uporabniki;
        }

        static List<Profil> NapolniSeznamProfilov()
        {
            var _profili = new List<Profil>();

            _profili.Add(new Profil() { Uporabnikid = 1, Filmid = 1 });
            _profili.Add(new Profil() { Uporabnikid = 1, Filmid = 2 });
            _profili.Add(new Profil() { Uporabnikid = 1, Filmid = 3 });
            _profili.Add(new Profil() { Uporabnikid = 1, Filmid = 4 });


            _profili.Add(new Profil() { Uporabnikid = 2, Filmid = 2 });
            _profili.Add(new Profil() { Uporabnikid = 2, Filmid = 4 });

            _profili.Add(new Profil() { Uporabnikid = 3, Filmid = 1 });
            _profili.Add(new Profil() { Uporabnikid = 3, Filmid = 3 });



            _profili.Add(new Profil() { Uporabnikid = 4, Filmid = 3 });
            _profili.Add(new Profil() { Uporabnikid = 4, Filmid = 4 });



            return _profili;
        }
        static void IzpisiFilme(List<Filmi> filmi)
        {



            foreach (var _f in filmi)
            {

                Console.WriteLine("Filmi id:" + _f.naslov);

            }


        }


        static void IzpisiUporabnike(List<Uporabik> uporabniki)
        {


            foreach (var _u in uporabniki)
            {

                Console.WriteLine("Uporabnik id:" + _u.ime);

            }


        }


        static void record_action(int uporabnikid, int filmid)
        {


            _profili.Add(new Profil() { Uporabnikid = uporabnikid, Filmid = filmid });
        }

        static void get_recommendations(int uporabnikid)
        {

            var gledaniFilmi = _profili
                .Where(p => p.Uporabnikid == uporabnikid)
                .Select(p => p.Filmid)
                .ToList();

            var podobniUporabniki = _profili
                .Where(p => gledaniFilmi.Contains(p.Filmid) && p.Uporabnikid != uporabnikid)
                .Select(p => p.Uporabnikid)
                .Distinct()
                .ToList();


            var priporoceniFilmi = _profili
                .Where(p => podobniUporabniki.Contains(p.Uporabnikid) && !gledaniFilmi.Contains(p.Filmid))
                .GroupBy(p => p.Filmid)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();



            Console.WriteLine($"Priporočila za uporabnika {uporabnikid}:");
            foreach (var filmId in priporoceniFilmi)
            {
                var film = _filmi.FirstOrDefault(f => f.id == filmId);
                if (film != null)
                {
                    Console.WriteLine($"Film ID: {film.id}, Naslov: {film.naslov}");
                }
            }
        }
        //static void get_recommendations(int uporabnikid)
        //{
        //    // Pridobite filme, ki jih je uporabnik že gledal
        //    var gledaniFilmi = _profili
        //        .Where(p => p.Uporabnikid == uporabnikid)
        //        .Select(p => p.Filmid)
        //        .ToList();


        //    var skupniprofili = _profili
        //    .Where(f => gledaniFilmi.Contains(f.Filmid) && f.Uporabnikid != uporabnikid) //poiščemo vse ki imajo film 1 in izločimo uporabnika(5)
        //    .Select(p => p.Uporabnikid)
        //    .ToList();

        //    var ostalifilmi = _profili
        //     .Where(f => skupniprofili.Contains(f.Uporabnikid) && f.Uporabnikid != uporabnikid)
        //     .ToList();

        //    // Filtrirajte filme, ki jih uporabnik še ni gledal
        //    var priporoceniFilmi = _filmi
        //        .Where(f => !gledaniFilmi.Contains(f.id))
        //        .ToList();




        //    // Izpišite priporočene filme
        //    Console.WriteLine($"Priporočila za uporabnika {uporabnikid}:");
        //    foreach (var film in priporoceniFilmi)
        //    {
        //        Console.WriteLine($"Film ID: {film.id}, Naslov: {film.naslov}");
        //    }
        //}

        //static void =mogoči uporabniku, da vnese podatke in pridobi priporočila.
        static void PredlagajNasledniFilm()
        {
            int _filmid = 0;

            int _userId = 1;
            while (_userId > 0)
            {
                Console.WriteLine("User id");
                _userId = int.Parse(Console.ReadLine());

                Console.WriteLine("Filem id");
                _filmid = int.Parse(Console.ReadLine());

                record_action(_userId, _filmid);

                get_recommendations(_userId);

            }



        }










    }

}
