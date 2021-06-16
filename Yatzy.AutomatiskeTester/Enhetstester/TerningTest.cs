using FluentAssertions;
using System;
using Xunit;
using Yatzy.Grensesnitt;
using Yatzy.Grensesnitt.DomeneModell;
using Yatzy.InternDomeneModell;

namespace Yatzy.AutomatiskeTester.EnhetsTester
{
    /// <summary>
    /// Enhetstest av klassen <see cref="Terning"/>. Klassen gjør mest validering av
    /// streng i sin konstruktor, så testen er enkel.
    /// </summary>
    public class TerningTest
    {
        [Fact]
        public void GyldigTerning()
        {
            //act
            Terning terning = new Terning("1", 1);
            //assert
            terning.Verdi.Should().Be(1);

            //act
            terning = new Terning("2", 1);
            //assert
            terning.Verdi.Should().Be(2);

            //act
            terning = new Terning("3", 1);
            //assert
            terning.Verdi.Should().Be(3);

            //act
            terning = new Terning("4", 1);
            //assert
            terning.Verdi.Should().Be(4);

            //act
            terning = new Terning("5", 1);
            //assert
            terning.Verdi.Should().Be(5);

            //act
            terning = new Terning("6", 1);
            //assert
            terning.Verdi.Should().Be(6);

        }

        [Fact]
        public void UgyldigTegnITerningStreng()
        {
            //act
            Action act = () => new Terning("as1", 1);
            //assert
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi 'as1'*");

            //act
            act = () => new Terning("0", 1);
            //assert
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi '0'*");

            //act
            act = () => new Terning("x", 1);
            //assert
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi 'x'*");
        }
    }
}
