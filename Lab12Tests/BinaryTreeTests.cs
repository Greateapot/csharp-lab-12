namespace Lab12Tests;

public class BinaryTreeTests
{

    [Fact]
    public void TestAddAndRemove()
    {
        // assign
        BinaryTree<Person> ints = new(capacity: 2);
        var p1 = Person.StRandomInit();
        var p2 = Person.StRandomInit();
        var p3 = Person.StRandomInit();
        bool[] expectedResult = [false];

        // act
        var addTwoElements = Record.Exception(() => ints.AddAll(p1, p2)); // ok; 2
        var addWithLimitReached = Record.Exception(() => ints.AddAll(p3)); // fail; cap
        ints.IsReadOnly = true;
        var addInRO = Record.Exception(() => ints.AddAll(p3)); // fail; ro
        var removeFromRO = Record.Exception(() => ints.RemoveAll(p3)); // fail ro
        ints.IsReadOnly = false;
        var removeThreeElements = Record.Exception(() => ints.RemoveAll(p2, p1)); // ok; 0
        var removeFromEmpty = ints.RemoveAll(p3); // false; 0
        ints.Add(p1); // ok; 1
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
        var p1 = Person.StRandomInit();
        var p2 = Person.StRandomInit();
        var p3 = Person.StRandomInit();
        var p4 = Person.StRandomInit();
        BinaryTree<Person> ints = [p1, p2, p3];

        // act
        var containsF = ints.Contains(p4); // false
        var containsT = ints.Contains(p3); // true
        // assert
        Assert.False(containsF);
        Assert.True(containsT);

        ints.Dispose();
    }

    [Fact]
    public void TestLeafs()
    {
        // assign
        BinaryTree<Person> ints = [];
        var p1 = new Person() { Surname = "1", Name = "1", Patronymic = "1", Age = 1 };
        var p2 = new Person() { Surname = "2", Name = "1", Patronymic = "1", Age = 1 };
        var p3 = new Person() { Surname = "3", Name = "1", Patronymic = "1", Age = 1 };
        var p4 = new Person() { Surname = "4", Name = "1", Patronymic = "1", Age = 1 };
        var p5 = new Person() { Surname = "5", Name = "1", Patronymic = "1", Age = 1 };
        var p6 = new Person() { Surname = "6", Name = "1", Patronymic = "1", Age = 1 };
        var p7 = new Person() { Surname = "7", Name = "1", Patronymic = "1", Age = 1 };
        var p32 = new Person() { Surname = "3", Name = "2", Patronymic = "1", Age = 1 };
        var p42 = new Person() { Surname = "4", Name = "2", Patronymic = "1", Age = 1 };
        var p52 = new Person() { Surname = "5", Name = "2", Patronymic = "1", Age = 1 };
        var p62 = new Person() { Surname = "6", Name = "2", Patronymic = "1", Age = 1 };
        var p72 = new Person() { Surname = "7", Name = "2", Patronymic = "1", Age = 1 };
        ints.AddAll(p1, p2, p3, p4, p5, p6, p7, p32, p42, p52, p62, p72);

        // act
        var leafs = ints.GetLeafCount(); // 6

        // assert 
        Assert.Equal(6, leafs);

        ints.Dispose();
    }

    [Fact]
    public void TestCloneAndEquals()
    {
        // assign
        BinaryTree<Person> ints = [];
        for (int i = 1; i < 9; i++)
            ints.Add(Person.StRandomInit());

        // act
        var clone = (BinaryTree<Person>)ints.Clone();
        var copy = ints.ShallowCopy();

        // assert 
        Assert.False(ints.Equals(clone));
        Assert.True(ints.Equals(copy));
        Assert.False(ints.Equals(1));

        ints.Dispose();
    }

    [Fact]
    public void TestRemovesAndDestructor()
    {
        // assign
        BinaryTree<Person> ints = [];
        var p1 = new Person() { Surname = "1", Name = "1", Patronymic = "1", Age = 1 };
        var p2 = new Person() { Surname = "2", Name = "1", Patronymic = "1", Age = 1 };
        var p3 = new Person() { Surname = "3", Name = "1", Patronymic = "1", Age = 1 };
        var p4 = new Person() { Surname = "4", Name = "1", Patronymic = "1", Age = 1 };
        var p5 = new Person() { Surname = "5", Name = "1", Patronymic = "1", Age = 1 };
        var p6 = new Person() { Surname = "6", Name = "1", Patronymic = "1", Age = 1 };
        var p7 = new Person() { Surname = "7", Name = "1", Patronymic = "1", Age = 1 };
        var p32 = new Person() { Surname = "3", Name = "2", Patronymic = "1", Age = 1 };
        var p42 = new Person() { Surname = "4", Name = "2", Patronymic = "1", Age = 1 };
        var p52 = new Person() { Surname = "5", Name = "2", Patronymic = "1", Age = 1 };
        var p62 = new Person() { Surname = "6", Name = "2", Patronymic = "1", Age = 1 };
        var p72 = new Person() { Surname = "7", Name = "2", Patronymic = "1", Age = 1 };
        ints.AddAll(p1, p2, p3, p4, p5, p6, p7, p32, p42, p52, p62, p72);

        // act
        var r = ints.RemoveAll(p1, p3, p5, p7, p2, p6, p4);

        // assert
        Assert.All(r, Assert.True);
    }


    [Fact]
    public void TestString()
    {
        // assign
        BinaryTree<Person> ints = [];
        var p1 = new Person() { Surname = "1", Name = "1", Patronymic = "1", Age = 1 };
        var p2 = new Person() { Surname = "2", Name = "1", Patronymic = "1", Age = 1 };
        var p3 = new Person() { Surname = "3", Name = "1", Patronymic = "1", Age = 1 };
        var p4 = new Person() { Surname = "4", Name = "1", Patronymic = "1", Age = 1 };
        ints.AddAll(p1, p2, p3, p4);
        var expectedStr = $"{p2}\n / {p1}\n \\ {p3}\n   \\ {p4}";

        // act
        var str = ints.ToString();

        // assert
        Assert.Contains(expectedStr, str);
    }
}

