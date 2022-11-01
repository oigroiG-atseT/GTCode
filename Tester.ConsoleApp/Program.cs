// See https://aka.ms/new-console-template for more information

using Tester.ConsoleApp.Generators.Realm.Cores;
using Tester.ConsoleApp.Generators.Realm.Models;

Console.WriteLine("\n\n\n");
Console.WriteLine("\t---TEST BATTERY LAUNCHING---\n");

var test001 = Test001_PropertyWrapperTestModel();
Console.WriteLine("\t+ Test001_PropertyWrapperTestModel: " + test001.SuccessString + "\t\t" + test001.Message);
var test002 = Test002_ClassWrapperTestModel();
Console.WriteLine("\t+ Test002_ClassWrapperTestModel: " + test002.SuccessString + "\t\t" + test002.Message);
var test003 = Test003_ValidatedPropertyWrapperTestModel();
Console.WriteLine("\t+ Test003_ValidatedPropertyWrapperTestModel: " + test003.SuccessString + "\t\t" + test003.Message);

Console.WriteLine("\n\t---TEST BATTERY ENDING---\n");
Console.WriteLine("\n\n\n");



Result Test001_PropertyWrapperTestModel()
{
    CoreInnerItem coreInnerItem = new CoreInnerItem() { Id = 1, IsActive = true, Level = 2, Name = "SonoInnerItem" };
    CoreItem coreItem = new CoreItem() { Id = 10, Level = 1, Name = "SonoCoreItem", InnerItem = coreInnerItem };
    try
    {
        var test = new PropertyWrapperTestModel(coreItem);
        bool assert =
            test.Id.Equals(coreItem.Id) &&
            test.Name.Equals(coreItem.Name) &&
            test.InnerName.Equals(coreInnerItem.Name) &&
            test.Item.Id.Equals(coreInnerItem.Id);
        if (assert is false) { return new Result(false, "[1] I valori del wrapper non coincidono con quelli contenuti nei core."); }

        test.InnerName = "InnerNameAggiornato";
        test.Item.Id = -1;
        test.Name = "CoreNameAggiornato";

        assert =
            test.Id.Equals(coreItem.Id) &&
            test.Name.Equals(coreItem.Name) &&
            test.InnerName.Equals(coreInnerItem.Name) &&
            test.Item.Id.Equals(coreInnerItem.Id);

        if (assert is false) { return new Result(false, "[2] I valori del wrapper non coincidono con quelli contenuti nei core."); }
    }
    catch (Exception e)
    {
        return new Result(false, "ERRORE:" + e.Message);
    }
    return new Result(true, "");
}

Result Test002_ClassWrapperTestModel()
{
    CoreInnerItem coreInnerItem = new CoreInnerItem() { Id = 1, IsActive = true, Level = 2, Name = "SonoInnerItem" };
    CoreItem coreItem = new CoreItem() { Id = 10, Level = 1, Name = "SonoCoreItem", InnerItem = coreInnerItem };
    try
    {
        var test = new ClassWrapperTestModel(coreItem);
        bool assert =
            test.Id.Equals(coreItem.Id) &&
            test.Level.Equals(coreItem.Level) &&
            test.InnerItem.Id.Equals(coreInnerItem.Id);
        if (assert is false) { return new Result(false, "[1] I valori del wrapper non coincidono con quelli contenuti nei core."); }

        test.Level = -1;
        test.InnerItem.Id = -1;
        test.InnerItem.Name = "InnerCoreNameAggiornato";

        assert =
            test.Level.Equals(-1) &&
            test.InnerItem.Id.Equals(-1) &&
            test.InnerItem.Name.Equals("InnerCoreNameAggiornato");

        if (assert is false) { return new Result(false, "[2] I valori del wrapper non sono stati aggiornati."); }

        var core = test.GetCore();

        assert =
            core.Level.Equals(-1) &&
            core.InnerItem.Id.Equals(-1) &&
            core.InnerItem.Name.Equals("InnerCoreNameAggiornato");

        if (assert is false) { return new Result(false, "[3] I valori del core non coincidono con quelli contenuti nel wrapper."); }
    }
    catch (Exception e)
    {
        return new Result(false, "ERRORE:" + e.Message);
    }
    return new Result(true, "");
}

Result Test003_ValidatedPropertyWrapperTestModel()
{
    CoreInnerItem coreInnerItem = new CoreInnerItem() { Id = 1, IsActive = true, Level = 2, Name = "SonoInnerItem" };
    CoreItem coreItem = new CoreItem() { Id = 10, Level = 1, Name = "SonoCoreItem", InnerItem = coreInnerItem };
    try
    {
        var test = new ValidatedPropertyWrapperTestModel(coreItem);
        bool assert =
            test.Id.Equals(coreItem.Id) &&
            test.Name.Equals(coreItem.Name);
           
        if (assert is false) { return new Result(false, "[1] I valori del wrapper non coincidono con quelli contenuti nei core."); }

        assert =
            test.Id.Equals(coreItem.Id) &&
            test.Name.Equals(coreItem.Name);            

        if (assert is false) { return new Result(false, "[2] I valori del wrapper non coincidono con quelli contenuti nei core."); }
        
    }
    catch (Exception e)
    {
        return new Result(false, "ERRORE:" + e.Message);
    }
    return new Result(true, "");
}

class Result
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public string SuccessString => Success ? "SUCCESS" : "FAIL";

    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }

}
