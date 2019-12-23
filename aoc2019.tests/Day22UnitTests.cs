using aoc2019.Code;
using System.Linq;
using Xunit;

public class Day22UnitTests
{
    [Theory]
    [InlineData(4485)]
    public void Day22_Part1(long part1)
    {
        var d = new Day22();

        Assert.Equal(part1.ToString(), d.Part1());
    }

    [Theory]
    [InlineData(91967327971097)]
    public void Day22_Part2(long part2)
    {
        var d = new Day22();

        Assert.Equal(part2.ToString(), d.Part2());
    }

    [Theory]
    [InlineData(10, new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
    public void Day22_Should_Deal_Into_New_Stack(int n, int[] cards)
    {
        var deck = new Day22.Deck(n);
        deck.DealNew();

        Assert.Equal(10, deck.Cards.Length);
        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(10, 3, new[] { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 })]
    [InlineData(10, -4, new[] { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 })]
    public void Day22_Should_Cut_N_cards(int n, int cut, int[] cards)
    {
        var deck = new Day22.Deck(n);
        deck.Cut(cut);

        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(10, 3, new[] { 0, 7, 4, 1, 8, 5, 2, 9, 6, 3 })]
    public void Day22_Should_Deal_With_Increment(int n, int deal, int[] cards)
    {
        var deck = new Day22.Deck(n);
        deck.Deal(deal);

        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(new[] { 0, 3, 6, 9, 2, 5, 8, 1, 4, 7 })]
    public void Day22_Should_Shuffle1(int[] cards)
    {
        var deck = new Day22.Deck(10);
        deck.Deal(7);
        deck.DealNew();
        deck.DealNew();

        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(new[] { 3, 0, 7, 4, 1, 8, 5, 2, 9, 6 })]
    public void Day22_Should_Shuffle2(int[] cards)
    {
        var deck = new Day22.Deck(10);
        deck.Cut(6);
        deck.Deal(7);
        deck.DealNew();

        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(new[] { 6, 3, 0, 7, 4, 1, 8, 5, 2, 9 })]
    public void Day22_Should_Shuffle3(int[] cards)
    {
        var deck = new Day22.Deck(10);
        deck.Deal(7);
        deck.Deal(9);
        deck.Cut(-2);

        Assert.True(deck.Cards.SequenceEqual(cards));
    }

    [Theory]
    [InlineData(new[] { 9, 2, 5, 8, 1, 4, 7, 0, 3, 6 })]
    public void Day22_Should_Shuffle4(int[] cards)
    {
        var deck = new Day22.Deck(10);
        deck.DealNew();
        deck.Cut(-2);
        deck.Deal(7);
        deck.Cut(8);
        deck.Cut(-4);
        deck.Deal(7);
        deck.Cut(3);
        deck.Deal(9);
        deck.Deal(3);
        deck.Cut(-1);

        Assert.True(deck.Cards.SequenceEqual(cards));
    }
}