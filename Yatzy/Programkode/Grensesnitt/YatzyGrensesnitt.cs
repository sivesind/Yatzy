using System;
using Yatzy.Grensesnitt.DomeneModell;
using Yatzy.InternDomeneModell;

namespace Yatzy.Grensesnitt
{
    /// <summary>
    /// For beskrivelse av hva metodene/tjenestene i denne klassen leverer: <see cref="IYatzyGrensesnitt"/>
    /// Dokumentasjon her beskriver kun noe om den interne virkemåten i metodene. Implementasjon
    /// er hovedsaklig noe logikk for å beregne høyeste mulig poengsum. 
    /// </summary>
    public class YatzyGrensesnitt : IYatzyGrensesnitt
    {
        /// <summary>
        /// Holder en referanse til det tilstandsløse objektet <see cref="PoengBeregning"/>, 
        /// for å gjøre poengberegninger.
        /// </summary>
        private readonly PoengBeregning _poengBeregning = new PoengBeregning();

        /// <summary>
        /// Det er stort sett klassen <see cref="Kast"/> som løser denne implementasjonen.
        /// <seealso cref="<seealso cref="IYatzyGrensesnitt.BeregnPoengSum(string, Kategori)"/>"/>
        public uint BeregnPoengSum(string femTerningVerdier, Kategori kategori)
        {
            Kast kast = new Kast(femTerningVerdier);
            return _poengBeregning.Beregn(kast, kategori);
        }

        /// <summary>
        /// Finner høyeste mulige poengsum ved å beregne poengsum for alle kategorier og ta vare 
        /// på den høyeste.
        /// <seealso cref="<seealso cref="IYatzyGrensesnitt.BeregnPoengSum(string, Kategori)"/>"/>
        public PoengOgKategoriMedHøyestPoengSum BeregnHøyesteMuligePoengSumForKast(string femTerningVerdier)
        {
            Kast kast = new Kast(femTerningVerdier);

            //starter med laveste mulige poengsum, 0
            uint sisteHøyestePoengSum = 0;
            Kategori sisteHøyestekategori = 0;
            //løp gjennom alle kategorier og husk den høyeste
            foreach (Kategori kategori in Enum.GetValues(typeof(Kategori)))
            {
                uint poengForGittKategori = _poengBeregning.Beregn(kast, kategori);
                //Hvis beregnede poeng er bedre enn tidligere huskes de. Bytter ikke kategori gitt at
                //man har samme poengsum. Designvalg her.
                if (poengForGittKategori > sisteHøyestePoengSum)
                {
                    sisteHøyestekategori = kategori;
                    sisteHøyestePoengSum = poengForGittKategori;
                }
            }
            //Bygg opp et returobjekt med beste verdier
            return new PoengOgKategoriMedHøyestPoengSum(sisteHøyestekategori, sisteHøyestePoengSum);
        }
    }
}
