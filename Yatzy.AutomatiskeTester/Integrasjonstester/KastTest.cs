using FluentAssertions;
using System;
using Xunit;
using Yatzy.Grensesnitt.DomeneModell;
using Yatzy.InternDomeneModell;

namespace Yatzy.AutomatiskeTester.IntegrasjonsTester
{
    /// <summary>
    /// Integrasjonstest av <see cref="Kast"/>. Dette er en integrasjonstest fordi man også berører
    /// funkjonalitet i <see cref="Terning"/>
    /// </summary>
    public class KastTest
    {
        [Fact]
        public void GyldigTegnIKaststreng()
        {
            var kast = new Kast("12345");
            kast.Terninger[0].Verdi.Should().Be(1);
            kast.Terninger[1].Verdi.Should().Be(2);
            kast.Terninger[2].Verdi.Should().Be(3);
            kast.Terninger[3].Verdi.Should().Be(4);
            kast.Terninger[4].Verdi.Should().Be(5);
        }

        [Fact]
        public void FeilengdeKaststreng()
        {
            Action act = () => new Kast("123456");
            act.Should().ThrowExactly<KastUgyldigUnntak>().WithMessage("*streng med lengde 5*");

            act = () => new Kast("123456");
            act.Should().ThrowExactly<KastUgyldigUnntak>().WithMessage("*streng med lengde 5*");

            act = () => new Kast("");
            act.Should().ThrowExactly<KastUgyldigUnntak>().WithMessage("*streng med lengde 5*");
        }

        [Fact]
        public void UgyldigTerningVerdiKaststreng()
        {
            Action act = () => new Kast("12347");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi '7'*");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*Terning nr 5*");

            act = () => new Kast("10345");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi '0'*");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*Terning nr 2*");

            act = () => new Kast("12x45");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*har verdi 'x'*");
            act.Should().ThrowExactly<TerningUgyldigUnntak>().WithMessage("*Terning nr 3*");
        }

    }
}
