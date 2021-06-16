using System;
using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.InternDomeneModell
{
    /// <summary>
    /// Klasse som representerer en kastet terning i spillet, det er stort sett
    /// validering i konstruktør som gjøres.
    /// </summary>
    internal class Terning
    {
        /// <summary>
        /// Terningens verdi, eller antall øyne. Kalles terningverdi internt i koden,
        /// da det ga noe bedre lesbarhet i navngivning.
        /// </summary>
        public uint Verdi { get; private set; }

        /// <summary>
        /// Validerer strengparameter for angitt terningverdi, og kaster unntak dersom den ikke 
        /// er gyldig.
        /// </summary>
        /// <param name="strengForTerningVerdi">Streng som angir forsøksvis terningverdi</param>
        /// <param name="kastNummerIRekken">Hvilket posisjon i en lengre streng denne terningverdien 
        /// har i kastet. Brukes for detaljert feilmelding.</param>
        /// <exception cref="TerningUgyldigUnntak">Dersom det er noe feil med en streng for terningverdi</exception>
        public Terning(string strengForTerningVerdi, uint kastNummerIRekken)
        {
            uint.TryParse(strengForTerningVerdi, out uint verdi);
            if (verdi < 1 || verdi > 6)
            {
                throw new TerningUgyldigUnntak(kastNummerIRekken, strengForTerningVerdi);
            }
            //verdien var gyldig
            Verdi = verdi;
        }
    }
}