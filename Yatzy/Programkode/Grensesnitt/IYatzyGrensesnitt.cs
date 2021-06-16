using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.Grensesnitt
{
    /// <summary>
    /// Dersom dette programbiblioteket skal benyttes i en 3. parts applikasjon
    /// er det alltid kunne kjekt å mocke det. Et c# Interface gjør mocking mye lettere,
    /// derfor tilbys et slik.
    /// </summary>
    public interface IYatzyGrensesnitt
    {
        /// <summary>
        /// Beregner høyeste mulige poengsum og tilhørende <see cref="Kategori"/> som er mulig å oppnå med et kast, 
        /// gitt fem terninger i ett kast.
        /// </summary>
        /// <param name="femTerningVerdier">En streng med 5 tallverdier mellom 1 og 6, som angir terningverdiene i et kast.</param>
        /// <exception cref="KastUgyldigUnntak">Dersom det er noe feil med lengden på strengen for terningverdier</exception>
        /// <exception cref="TerningUgyldigUnntak">Dersom det er noe feil med en enkelt terning innenfor strengen av terningverdier</exception>
        /// <returns>Høyeste mulige poengsum og tilhørende <see cref="Kategori"/> som er mulig å oppnå med et kast, 
        /// gitt fem terninger i ett kast. Returdata kapsles inn i et objekt av typen <see cref="PoengOgKategoriMedHøyestPoengSum"/></returns>
        PoengOgKategoriMedHøyestPoengSum BeregnHøyesteMuligePoengSumForKast(string femTerningVerdier);

        /// <summary>
        /// En ren beregningstjeneste for et terningkast.
        /// </summary>
        /// <param name="femTerningVerdier">En streng med 5 tallverdier mellom 1 og 6, som angir terningverdiene i et kast.</param>
        /// <param name="kategori">Kategori som poeng i kastet skal beregnes for</param>
        /// <exception cref="KastUgyldigUnntak">Dersom det er noe feil med lengden på strengen for terningverdier</exception>
        /// <exception cref="TerningUgyldigUnntak">Dersom det er noe feil med en enkelt terning innenfor strengen av terningverdier</exception>
        /// <returns>Poengsum for kastet med tilhørende kategori, 0 dersom kastet ikke gir poeng for angitt kategori.</returns>
        uint BeregnPoengSum(string femTerningVerdier, Kategori kategori);
    }
}