using FluentAssertions;
using Xunit;
using Yatzy.Grensesnitt.DomeneModell;
using Yatzy.InternDomeneModell;

namespace Yatzy.AutomatiskeTester.EnhetsTester
{
    /// <summary>
    /// Enhetstester for klassen <see cref="PoengBeregning"/> Det er mange kombinasjoner i Yatzy, 
    /// testene her forsøker å fange det essensielle av positive og negative tester. I en produksjonsløsning 
    /// ville jeg utvidet testene betydelig, dette er mer å regne som et eksempel.
    /// </summary>
    public class PoengBeregningTest
    {
        private readonly PoengBeregning _poengBeregning = new PoengBeregning();

        [Fact]
        public void TestEnkle()
        {
            _poengBeregning.Beregn(new Kast("11234"), Kategori.Enere).Should().Be(2);
            _poengBeregning.Beregn(new Kast("11224"), Kategori.Toere).Should().Be(4);
            _poengBeregning.Beregn(new Kast("33234"), Kategori.Treere).Should().Be(9);
            _poengBeregning.Beregn(new Kast("14234"), Kategori.Firere).Should().Be(8);
            _poengBeregning.Beregn(new Kast("15554"), Kategori.Femmere).Should().Be(15);
            _poengBeregning.Beregn(new Kast("16634"), Kategori.Seksere).Should().Be(12);
            _poengBeregning.Beregn(new Kast("26634"), Kategori.Enere).Should().Be(0);

        }

        [Fact]
        public void TestPar()
        {
            _poengBeregning.Beregn(new Kast("12345"), Kategori.Par).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11345"), Kategori.Par).Should().Be(2);
            _poengBeregning.Beregn(new Kast("11225"), Kategori.Par).Should().Be(4);
            _poengBeregning.Beregn(new Kast("11222"), Kategori.Par).Should().Be(4);
        }

        [Fact]
        public void TestToPar()
        {
            _poengBeregning.Beregn(new Kast("11112"), Kategori.ToPar).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11223"), Kategori.ToPar).Should().Be(6);
            _poengBeregning.Beregn(new Kast("11222"), Kategori.ToPar).Should().Be(6);
        }

        [Fact]
        public void TestTreLike()
        {
            _poengBeregning.Beregn(new Kast("11342"), Kategori.TreLike).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11112"), Kategori.TreLike).Should().Be(3);
            _poengBeregning.Beregn(new Kast("12223"), Kategori.TreLike).Should().Be(6);
            _poengBeregning.Beregn(new Kast("11222"), Kategori.TreLike).Should().Be(6);
        }

        [Fact]
        public void TestFireLike()
        {
            _poengBeregning.Beregn(new Kast("11111"), Kategori.FireLike).Should().Be(4);
            _poengBeregning.Beregn(new Kast("11142"), Kategori.FireLike).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11121"), Kategori.FireLike).Should().Be(4);
            _poengBeregning.Beregn(new Kast("12222"), Kategori.FireLike).Should().Be(8);
            _poengBeregning.Beregn(new Kast("11222"), Kategori.FireLike).Should().Be(0);
        }

        [Fact]
        public void TestLitenStraight()
        {
            _poengBeregning.Beregn(new Kast("11142"), Kategori.LitenStraight).Should().Be(0);
            _poengBeregning.Beregn(new Kast("53241"), Kategori.LitenStraight).Should().Be(15);
            _poengBeregning.Beregn(new Kast("23456"), Kategori.LitenStraight).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11345"), Kategori.LitenStraight).Should().Be(0);
        }

        [Fact]
        public void TestStorStraight()
        {
            _poengBeregning.Beregn(new Kast("11142"), Kategori.StorStraight).Should().Be(0);
            _poengBeregning.Beregn(new Kast("23456"), Kategori.StorStraight).Should().Be(20);
            _poengBeregning.Beregn(new Kast("32645"), Kategori.StorStraight).Should().Be(20);
            _poengBeregning.Beregn(new Kast("12345"), Kategori.StorStraight).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11345"), Kategori.StorStraight).Should().Be(0);
            _poengBeregning.Beregn(new Kast("12346"), Kategori.StorStraight).Should().Be(0);
        }

        [Fact]
        public void TestFulltHus()
        {
            _poengBeregning.Beregn(new Kast("11122"), Kategori.FulltHus).Should().Be(7);
            _poengBeregning.Beregn(new Kast("12121"), Kategori.FulltHus).Should().Be(7);
            _poengBeregning.Beregn(new Kast("22121"), Kategori.FulltHus).Should().Be(8);
            _poengBeregning.Beregn(new Kast("11223"), Kategori.FulltHus).Should().Be(0);
            _poengBeregning.Beregn(new Kast("11111"), Kategori.FulltHus).Should().Be(0);
        }

        [Fact]
        public void TestSjanse()
        {
            _poengBeregning.Beregn(new Kast("11111"), Kategori.Sjanse).Should().Be(5);
            _poengBeregning.Beregn(new Kast("12121"), Kategori.Sjanse).Should().Be(7);
            _poengBeregning.Beregn(new Kast("22121"), Kategori.Sjanse).Should().Be(8);
            _poengBeregning.Beregn(new Kast("11223"), Kategori.Sjanse).Should().Be(9);
        }

        [Fact]
        public void TestYatzy()
        {
            _poengBeregning.Beregn(new Kast("11111"), Kategori.Yatzy).Should().Be(50);
            _poengBeregning.Beregn(new Kast("11112"), Kategori.Yatzy).Should().Be(0);
            _poengBeregning.Beregn(new Kast("22222"), Kategori.Yatzy).Should().Be(50);
            _poengBeregning.Beregn(new Kast("33333"), Kategori.Yatzy).Should().Be(50);
        }


    }
}
