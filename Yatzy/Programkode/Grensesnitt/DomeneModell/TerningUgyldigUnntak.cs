using System;
using System.Runtime.Serialization;

namespace Yatzy.Grensesnitt.DomeneModell
{
    /// <summary>
    /// Unntak som kastes når det forsøkes å angi en terning i et kast med en ugyldig 
    /// verdi. Feilmelding i unntaket angir hva som var ugyldig med terningverdien.
    /// FD: Utvide med feilkoder dersom dette skulle vært et produksjonsklart API.
    /// </summary>
    [Serializable]
    internal class TerningUgyldigUnntak : Exception
    {
        /// <summary>
        /// Skaper et unntak med angitte parametre for å formatere feilmelding.
        /// </summary>
        /// <param name="terningNummerIRekken">Hvilket nummer i rekken av en string terningen var angitt</param>
        /// <param name="strengForTerningVerdi">Streng som ble forsøkt brukt for å angi terningverdien</param>
        public TerningUgyldigUnntak(uint terningNummerIRekken, string strengForTerningVerdi): base(LagFeilmelding(terningNummerIRekken, strengForTerningVerdi))
        {
        }

        /// <summary>
        /// Hjelpemetode for å skape feilmelding. Static for å kunne brukes fra konstruktør-viderekall
        /// i konstruktør over.
        /// </summary>
        /// <param name="terningNummerIRekken">Hvilket nummer i rekken av en string terningen var angitt</param>
        /// <param name="strengForTerningVerdi">Streng som ble forsøkt brukt for å angi terningverdien</param>
        /// <returns></returns>
        private static string LagFeilmelding(uint terningNummerIRekken, string strengForTerningVerdi)
        {
            return $"Terning nr {terningNummerIRekken} har verdi '{strengForTerningVerdi}', men må være et tall mellom 1 og 6";
        }
    }
}