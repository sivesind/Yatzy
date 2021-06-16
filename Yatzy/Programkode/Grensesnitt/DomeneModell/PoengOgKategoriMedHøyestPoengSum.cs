using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.Grensesnitt.DomeneModell
{
    /// <summary>
    /// Uforanderlig klasse som brukes som svar på tjenesten <see cref="IYatzyGrensesnitt.BeregnHøyesteMuligePoengSumForKast(string)"/>
    /// </summary>
    public class PoengOgKategoriMedHøyestPoengSum
    {
        /// <summary>
        /// Skaper en instans av denne klassen ved å sette dens to verdier.
        /// </summary>
        /// <param name="optimalKategori">Kategorien som gir høyest poengsum for et kast.</param>
        /// <param name="poengSum">Oppnådd poengsum for angitt kategori</param>
        public PoengOgKategoriMedHøyestPoengSum(Kategori optimalKategori, uint poengSum)
        {
            Poeng = poengSum;
            KategoriMedHøyestPoengSum = optimalKategori;
        }

        /// <summary>
        /// Oppnådd poengsum for angitt kategori
        /// </summary>
        public uint Poeng { get; }
        /// <summary>
        /// Kategorien som gir høyest poengsum for et kast.
        /// </summary>
        public Kategori KategoriMedHøyestPoengSum { get; }
    }
}
