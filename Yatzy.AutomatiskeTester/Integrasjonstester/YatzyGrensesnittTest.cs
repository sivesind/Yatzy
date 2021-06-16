using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yatzy.Grensesnitt;
using Yatzy.Grensesnitt.DomeneModell;

namespace Yatzy.AutomatiskeTester.IntegrasjonsTester
{
    /// <summary>
    /// Integrasjonstest av selve hovedgrensesnittet i programpakken. Pakken tilbyr en ren poengberegning samt
    /// en tjeneste for å finne kastet som gir mest poeng. Det er mange kombinasjoner i Yatzy, testene her 
    /// forsøker å fange det essensielle av positive og negative tester.
    /// </summary>
    public class YatzyGrensesnittTest
    {
        /// <summary>
        /// Klassen som testes.
        /// </summary>
        private IYatzyGrensesnitt _yatzy = new YatzyGrensesnitt();

        /// <summary>
        /// Deter mulig jeg har misforstått krav, men denne vil nesten alltid slå ut på Sjanse. Dersom 
        /// spillet hadde "brukt opp" kastene ville den vært mer interessant. 
        /// </summary>
        [Fact]
        public void BeregnHøyesteMuligePoengSumForKastTest()
        {
            _yatzy.BeregnHøyesteMuligePoengSumForKast("11111").Poeng.Should().Be(50);
            _yatzy.BeregnHøyesteMuligePoengSumForKast("11111").KategoriMedHøyestPoengSum.Should().Be(Kategori.Yatzy);

            _yatzy.BeregnHøyesteMuligePoengSumForKast("11112").Poeng.Should().Be(6);
            _yatzy.BeregnHøyesteMuligePoengSumForKast("11112").KategoriMedHøyestPoengSum.Should().Be(Kategori.Sjanse);

            _yatzy.BeregnHøyesteMuligePoengSumForKast("11221").Poeng.Should().Be(7);
            _yatzy.BeregnHøyesteMuligePoengSumForKast("11221").KategoriMedHøyestPoengSum.Should().Be(Kategori.FulltHus);

            _yatzy.BeregnHøyesteMuligePoengSumForKast("12345").Poeng.Should().Be(15);
            _yatzy.BeregnHøyesteMuligePoengSumForKast("12345").KategoriMedHøyestPoengSum.Should().Be(Kategori.LitenStraight);

            _yatzy.BeregnHøyesteMuligePoengSumForKast("23456").Poeng.Should().Be(20);
            _yatzy.BeregnHøyesteMuligePoengSumForKast("23456").KategoriMedHøyestPoengSum.Should().Be(Kategori.StorStraight);
        }


        /// <summary>
        /// Dette er allerede stort sett testet i integrasjonstest for <see cref="Kast"/> og 
        /// enhetstest for <see cref="PoengBeregning"/>, men greit å ha med en smoketest
        /// for beregningen
        /// </summary>
        [Fact]
        public void PoengBeregningSmokeTest()
        {
            _yatzy.BeregnPoengSum("11111", Kategori.Enere).Should().Be(5);
            _yatzy.BeregnPoengSum("11111", Kategori.Yatzy).Should().Be(50);
            _yatzy.BeregnPoengSum("11114", Kategori.Firere).Should().Be(4);
            _yatzy.BeregnPoengSum("11122", Kategori.FulltHus).Should().Be(7);
            _yatzy.BeregnPoengSum("11111", Kategori.Sjanse).Should().Be(5);
            _yatzy.BeregnPoengSum("44556", Kategori.ToPar).Should().Be(18);
        }
    }
}
