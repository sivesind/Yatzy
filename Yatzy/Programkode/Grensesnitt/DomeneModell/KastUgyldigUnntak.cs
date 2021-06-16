using System;

namespace Yatzy.Grensesnitt.DomeneModell
{
    /// <summary>
    /// Unntak kastes dersom en streng som angir et kast har ugyldiog format. Unntaksmelding 
    /// sier noe om hva som var feil med strengen.
    /// FD: Utvide med feilkoder dersom dette skulle vært et produksjonsklart API.
    /// </summary>
    [Serializable]
    internal class KastUgyldigUnntak : Exception
    {
        /// <summary>
        /// Trenger bare å kalle videre for å sette unntaksmelding i baseklasse.
        /// </summary>
        /// <param name="message">Feilmelding for unntak</param>
        public KastUgyldigUnntak(string message) : base(message)
        {
        }
    }
}