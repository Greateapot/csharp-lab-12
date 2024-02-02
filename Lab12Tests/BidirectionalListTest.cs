namespace Lab12Tests;

public class BidirectionalListTest
{
    [Fact]
    public void TestAddAndRemove()
    {
        // assign
        BidirectionalList<int> ints = new(capacity: 3);
        bool[] expectedResult = [false];

        // act
        var addTwoElements = Record.Exception(() => ints.AddAll(1, 2)); // ok; 2
        var addAfterInvalid = Record.Exception(() => ints.AddAfter(3, 3)); // fail; not found
        var addAfterValid = Record.Exception(() => ints.AddAfter(2, 3)); // ok; 3
        var addWithLimitReached = Record.Exception(() => ints.AddAll(4)); // fail; cap
        ints.IsReadOnly = true;
        var addInRO = Record.Exception(() => ints.AddAll(5)); // fail; ro
        var removeFromRO = Record.Exception(() => ints.RemoveAll(4)); // fail ro
        ints.IsReadOnly = false;
        var removeThreeElements = Record.Exception(() => ints.RemoveAll(3, 2, 1)); // ok; 0
        var removeFromEmpty = ints.RemoveAll(3); // false; 0
        ints.Add(1); // ok; 1
        ints.Clear(); // ok; 0

        // assert
        Assert.Null(addTwoElements);
        Assert.IsType<ArgumentException>(addAfterInvalid);
        Assert.Null(addAfterValid);
        Assert.IsType<CollectionIsFullException>(addWithLimitReached);
        Assert.IsType<CollectionIsReadOnlyException>(addInRO);
        Assert.IsType<CollectionIsReadOnlyException>(removeFromRO);
        Assert.Null(removeThreeElements);
        Assert.Equal(expectedResult, removeFromEmpty);
        Assert.Empty(ints);

        ints.Dispose();
    }

    [Fact]
    public void TestContainsAndCopy()
    {
        // assign
        BidirectionalList<int> ints = [1, 2, 3];
        var expectedArray = new int[3] { 1, 2, 3 };
        var array = new int[3];

        // act
        var containsF = ints.Contains(4); // false
        var containsT = ints.Contains(3); // true
        ints.CopyTo(array, 0);

        // assert
        Assert.False(containsF);
        Assert.True(containsT);
        Assert.Equal(expectedArray, array);
        
        ints.Dispose();
    }

    [Fact]
    public void TestString()
    {
        // assign
        BidirectionalList<int> ints = [1, 2, 3];
        var expectedString = "1. 1\n2. 2\n3. 3";

        // act
        var actualString = ints.ToString(); // 1. 1\n2. 2\n3. 3

        // assert 
        Assert.Contains(expectedString, actualString);
        
        ints.Dispose();
    }
}