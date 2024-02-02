namespace Lab12Tests;

public class BinaryTreeTest
{

    [Fact]
    public void TestAddAndRemove()
    {
        // assign
        BinaryTree<int> ints = new(capacity: 2);
        bool[] expectedResult = [false];

        // act
        var addTwoElements = Record.Exception(() => ints.AddAll(1, 2)); // ok; 2
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
        BinaryTree<int> ints = [1, 2, 3];
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
        BinaryTree<int> ints = [1, 2, 3];
        var expectedString = "2\n / 1\n \\ 3";

        // act
        var actualString = ints.ToString(); // 2\n / 1\n \\ 3

        // assert 
        Assert.Contains(expectedString, actualString);

        ints.Dispose();
    }

    [Fact]
    public void TestLeafs()
    {
        // assign
        BinaryTree<int> ints = [];
        for (int i = 1; i < 10; i++)
            ints.Add(i);

        // act
        var leafs = ints.GetLeafCount(); //  4

        // assert 
        Assert.Equal(5, leafs);

        ints.Dispose();
    }
}
