using System;
using System.Collections.Generic;
using System.Linq;
using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.InternDomeneModell
{
    /// <summary>
    /// Beregninger av poeng er implementert ved en metode pr kategori. Innenfor poengbereningsmetodene 
    /// er det brukt ulike fremgangsmåter for bereging, de fleste er basert på gruppering med Linq. Linq 
    /// egner seg greit for oppgaven. Når poengberegning kjøres vet man at kastet/terningene er gyldige,
    /// det brukes som forutsetning i beregninger. Trenger ikke derfor ikke noe unntakshåndtering.
    /// </summary>
    internal class PoengBeregning
    {
        /// <summary>
        /// Inngangsmetode for beregning som kaller rett beregningsmetode basert på kategori.
        /// </summary>
        /// <param name="kast">En liste av <see cref="Terning"/> med 5 verdier mellom 1 og 6, som angir gyldige terningverdier i kastet.</param>
        /// <param name="kategori">Kategori som poeng i kastet skal beregnes for</param>
        /// <returns>Poeng for kastet, 0 dersom det ikke gir poeng</returns>
        public uint Beregn(Kast kast, Kategori kategori)
        {
            //enkel løsning for å mappe fra kategori til beregningsmetode
            switch (kategori)
            {
                case Kategori.Enere:
                    return Enere(kast);
                case Kategori.Toere:
                    return Toere(kast);
                case Kategori.Treere:
                    return Treere(kast);
                case Kategori.Firere:
                    return Firere(kast);
                case Kategori.Femmere:
                    return Femmere(kast);
                case Kategori.Seksere:
                    return Seksere(kast);
                case Kategori.Par:
                    return Par(kast);
                case Kategori.ToPar:
                    return ToPar(kast);
                case Kategori.TreLike:
                    return TreLike(kast);
                case Kategori.FireLike:
                    return FireLike(kast);
                case Kategori.LitenStraight:
                    return LitenStraight(kast);
                case Kategori.StorStraight:
                    return StorStraight(kast);
                case Kategori.FulltHus:
                    return FulltHus(kast);
                case Kategori.Sjanse:
                    return Sjanse(kast);
                case Kategori.Yatzy:
                    return Yatzy(kast);
                default:
                    throw new Exception($"Uventet feil for ukjent Kategori {kategori}, ta kontakt med sivesind@gmail.com for bistand");
            }
        }

        /// <summary>
        /// Dersom det er 5 like i kastet gir det 50 poeng, 0 ellers.
        /// </summary>
        private uint Yatzy(Kast kast)
        {
            return FinnTerningVerdiMedMinimumForekomsterIKastet(kast, 5).Count > 0 ? (uint)50 : 0;
        }

        /// <summary>
        /// Summerer samtlige terninger.
        /// </summary>
        private uint Sjanse(Kast kast)
        {
            return (uint)kast.Terninger.Sum(terning => terning.Verdi);

        }

        /// <summary>
        /// Sjekker først om det finnes tre eller flere like, deretter om det finnes eksakt to like. Forekomst 
        /// av tre eller flere like kan logisk ikke ha samme verdi som eksakt to like. Om begge finnes må 
        /// det være hus i kastet.
        /// </summary>
        private uint FulltHus(Kast kast)
        {
            //finn forekomst av tre eller flere like
            uint terningVerdiMedTreEllerFlereForekomster = FinnTerningVerdiMedMinimumForekomsterIKastet(kast, 3).FirstOrDefault();
            //finn forekomst eksakt to like
            uint terningVerdiMedEksaktToForekomster = kast.Terninger.GroupBy(terning => terning.Verdi)
                           .Where(gruppering => gruppering.Count() == 2)
                           //legg terningverdi i resultatliste
                           .Select(gruppering => gruppering.Key)
                           .FirstOrDefault();
            return
                ((terningVerdiMedTreEllerFlereForekomster > 0)
                &&
                (terningVerdiMedEksaktToForekomster > 0)
                ?
                //det finnes både tre like og to like, beregn poeng
                (terningVerdiMedTreEllerFlereForekomster * 3) + (terningVerdiMedEksaktToForekomster * 2)
                :
                //det var ikke både to like og tre like i kastet, 0 poeng
                0);
        }

        /// <summary>
        /// Sjekker ved å sortere terningverdiene og sjekke eksakte verdier. Straight 
        /// er en unik terningkombinasjon.
        /// </summary>
        private uint StorStraight(Kast kast)
        {
            var sorterteTerninger = kast.SorterTerningerLavestVerdiFørst();
            return (
                 (sorterteTerninger[0].Verdi == 2)
                 && (sorterteTerninger[1].Verdi == 3)
                 && (sorterteTerninger[2].Verdi == 4)
                 && (sorterteTerninger[3].Verdi == 5)
                 && (sorterteTerninger[4].Verdi == 6)
                 ) ?
                 (uint)20 : 0;
        }

        /// <summary>
        /// Sjekker ved å sortere terningverdiene og sjekke eksakte verdier. Straight 
        /// er en unik terningkombinasjon.
        /// </summary>
        private uint LitenStraight(Kast kast)
        {
            var sorterteTerninger = kast.SorterTerningerLavestVerdiFørst();
            return (
                 (sorterteTerninger[0].Verdi == 1)
                 && (sorterteTerninger[1].Verdi == 2)
                 && (sorterteTerninger[2].Verdi == 3)
                 && (sorterteTerninger[3].Verdi == 4)
                 && (sorterteTerninger[4].Verdi == 5)
                 ) ?
                 (uint)15 : 0;
        }

        /// <summary>
        /// Sjekker at det er minst fire like i kastet, med <seealso cref="FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(Kast, uint)"/>
        /// </summary>
        private uint FireLike(Kast kast)
        {
            return FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(kast, 4);
        }

        /// <summary>
        /// Sjekker at det er minst tre like i kastet, med <seealso cref="FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(Kast, uint)"/>
        /// </summary>
        private uint TreLike(Kast kast)
        {
            return FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(kast, 3);
        }

        /// <summary>
        /// Finner liste av terninger med par av verdier vha <seealso cref="FinnTerningVerdiMedMinimumForekomsterIKastet(Kast, uint)"/>
        /// og summerer poeng.
        /// </summary>
        private uint ToPar(Kast kast)
        {
            //finn to par ved å gruppere på terninger med minst 2 like terningverdier
            List<uint> listeTerningVerdierMedToForekomsterHver = FinnTerningVerdiMedMinimumForekomsterIKastet(kast, 2);

            //hvis ikke to grupper/par får man null poeng
            return
                (listeTerningVerdierMedToForekomsterHver.Count < 2)
                ?
                0 :
                //det var to grupper/par, da kan poeng beregnes
                (listeTerningVerdierMedToForekomsterHver[0] * 2) + (listeTerningVerdierMedToForekomsterHver[1] * 2);
        }

        /// <summary>
        /// Finner par med <seealso cref="FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(Kast, uint)"/>
        /// </summary>
        private uint Par(Kast kast)
        {
            return FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(kast, 2);
        }

        /// <summary>
        /// Summerer seksere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>
        private uint Seksere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 6);
        }

        /// <summary>
        /// Summerer femmere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>
        private uint Femmere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 5);
        }

        /// <summary>
        /// Summerer firere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>
        private uint Firere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 4);
        }

        /// <summary>
        /// Summerer Treere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>

        private uint Treere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 3);
        }

        /// <summary>
        /// Summerer toere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>
        private uint Toere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 2);
        }

        /// <summary>
        /// Summerer enere med <seealso cref="SummérTerningerMedGittAntallØyne(Kast, uint)"/>
        /// </summary>
        private uint Enere(Kast kast)
        {
            return SummérTerningerMedGittAntallØyne(kast, 1);
        }

        /// <summary>
        /// Hjelpemetode for å summere ternigner med en gitt verdi i kastet. Brukes
        /// på de enkle startkomninasjonene.
        /// </summary>
        /// <param name="kast">Kastet med terninger som skal summeres</param>
        /// <param name="terningVerdiÅSummereFor">F.eks. 5 summerer for femmere i kastet</param>
        /// <returns>Sum av angitt terningverdi, 0 derom angitt verdi ikke finnes i kastet.</returns>
        private uint SummérTerningerMedGittAntallØyne(Kast kast, uint terningVerdiÅSummereFor)
        {
            uint sum = 0;
            foreach (Terning terning in kast.Terninger)
            {
                if (terning.Verdi == terningVerdiÅSummereFor)
                {
                    sum += terningVerdiÅSummereFor;
                }
            }
            return sum;
        }

        private uint FinnMinstSåMangeLikeTerningerIKastetOgSummerHøyeste(Kast kast, uint minstSåMangeLikeIKastet)
        {
            //finn par ved å gruppere på terninger med 2 eller flere like øyne
            List<uint> listeØyneTypeAntallMedMinimumPar = FinnTerningVerdiMedMinimumForekomsterIKastet(kast, minstSåMangeLikeIKastet);

            //sorter for å velge høyeste derom det er poenggivende kast
            listeØyneTypeAntallMedMinimumPar.Sort();
            return
                (listeØyneTypeAntallMedMinimumPar.Count == 0)
                ?
                //det var ikke nok like
                0
                :
                //returner høyeste verdi * antall like i kastet
                listeØyneTypeAntallMedMinimumPar.Last() * minstSåMangeLikeIKastet;
        }

        private List<uint> FinnTerningVerdiMedMinimumForekomsterIKastet(Kast kast, uint minimumAntallGangerIKastet)
        {
            //Linq lager grupper av terningverdier i kastet, basert på et ønsket minimum antall i gruppen(e)
            return kast.Terninger.GroupBy(terning => terning.Verdi)
                    .Where(gruppering => gruppering.Count() >= minimumAntallGangerIKastet)
                    //legg terningverdi for gruppen(e) i resultatliste
                    .Select(gruppering => gruppering.Key)
                    .ToList();
        }
    }
}
