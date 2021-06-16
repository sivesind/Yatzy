using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.InternDomeneModell
{
    /// <summary>
    /// Domeneobjekt for å representere et kast i Yatzy. Implementanetasjon består hovedsaklig
    /// av å validere en streng som skal representere et kast med fem terninger.
    /// </summary>
    internal class Kast
    {
        /// <summary>
        /// Privat liste av fem <see cref="Terning"/> objekter som representerer hver terning i kastet. Privat 
        /// for å bevare tilstand på kastet.
        /// </summary>
        private readonly List<Terning> _terninger = new List<Terning>(5);

        /// <summary>
        /// Opprinnelig kast med hver sin <see cref="Terning"/>
        /// </summary>
        public readonly ReadOnlyCollection<Terning> Terninger;

        /// <summary>
        /// Validerer streng for terningverdier (antall øyne). 
        /// </summary>
        /// <param name="femTerningVerdierSomString"></param>
        /// <param name="kategori"></param>
        /// <exception cref="KastUgyldigUnntak">Dersom det er noe feil med lengden på strengen for terningverdier</exception>
        /// <exception cref="TerningUgyldigUnntak">Dersom det er noe feil med en enkelt terning innenfor strengen av terningverdier</exception>
        public Kast(string femTerningVerdierSomString)
        {
            Terninger = new ReadOnlyCollection<Terning>(_terninger);

            //sjekk gyldighet av kast
            if (femTerningVerdierSomString.Length != 5)
            {
                throw new KastUgyldigUnntak("Kast må inneholde verdi fra 5 terninger, i en streng med lengde 5, og 5 tall mellom 1 og 6 i hvert tegn");
            }

            //her vet vi at strengen har riktig lengde, sjekker hver terningverdi
            for (int terningNummer = 1; terningNummer < 6; terningNummer++)
            {
                _terninger.Add(new Terning(femTerningVerdierSomString[terningNummer - 1].ToString(), (uint)terningNummer));
            }
        }

        /// <summary>
        /// Hjelpemetode for å trekke ut en sortert liste basert på terningverdiene. Brukes 
        /// til hjelp når poeng skal beregnes.
        /// </summary>
        /// <returns>En <see cref="List{T}"/> av terninger, sortert med laveste terningverdi først.</returns>
        public List<Terning> SorterTerningerLavestVerdiFørst()
        {
            return _terninger.OrderBy(terning => terning.Verdi).ToList();
        }
    }
}