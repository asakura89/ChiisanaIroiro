<!--
SPDX-License-Identifier: 0BSD
-->

# Ayumi

## License

Dilisensikan di bawah BSD Zero Clause License. Atau liat [LICENSE file ini](LICENSE.md), atau buka link ini https://opensource.org/licenses/0BSD buat keterangan lengkap.

[SPDX](https://spdx.dev) license identifier buat project ini `0BSD`.

Untuk hal lain yang punya license berbeda, silakan baca license link yang disertakan.



## Keywielder

Simple token, unique key generator. It's one of my favorite library to generate formatted primary key.
Such as Document number in (LOB) apps.



### How to - 1

```csharp
String key = Wielder
    .New()
    .AddString("KEY")
    .AddString("-")
    .AddLongYear()
    .AddNumericMonth()
    .AddDate()
    .AddString("-")
    .AddLeftPadded(w => w.AddCounter(12, 10), 4, '0')
    .BuildKey()
    .Dump();

Console.WriteLine(key);
```

Above code will generate key like this (below), result may vary as I use date
*KEY-20150125-0022*



## KeyBlaster

Similar to KeyWielder but more simple


> ❗ Note
> 
> Old documentation


Note: Keyblaster has been merged to Keywielder and become Random-something and GUIDString

### How to

```csharp
String simpleKey = Keywielder
        .New()
        .AddString("SIMPLE", 5, "-")
        .AddGUIDString("-")
        .AddLongYear("-")
        .AddRandomAlphaNumeric(10, "-")
        .AddRandomString(5, "-")
        .AddNumericDay()
        .BuildKey();

Console.WriteLine(complexKey);
```

Above code will generate key like this (below), result may vary as I use year and combination random and GUID
*SIMPL-3d5968351a0645dda7bfd3bbeb4ad972-2015-CRFWSJWZ3B-SPRHG-01*



Note: Don't forget to edit Post-Build event in Project properties



### How to - 2

```csharp
String complexKey = Wielder
    .New()
    .AddString("SIMPLE")
    .AddString("-")
    .AddGuidString()
    .AddString("-")
    .AddLongYear()
    .AddString("-")
    .AddRandomAlphaNumeric(10)
    .AddString("-")
    .AddRandomString(5)
    .AddString("-")
    .AddNumericDay()
    .BuildKey()
    .Dump();

Console.WriteLine(complexKey);
```

Above code will generate key like this (below), result may vary as I use year and combination random and GUID
*SIMPL-3d5968351a0645dda7bfd3bbeb4ad972-2015-CRFWSJWZ3B-SPRHG-01*



## Databossy

Query your data like a boss. With simplicity and control.

### 1. Open connection

```csharp
using (var db = new Database()) {
    // .. query data
}
```

Call new Database without parameter, it will use *System.Data.SqlClient* provider.
Call with one parameter, it will open connection with connection string name as parameter.
And if you want to use connection string instead of name, you should call it with connection string and
ConnectionStringType as parameters



### 2. Get data in DataTable or Generic List it's your call
```csharp
using (var db = new Database()) {
    return db.Query("SELECT * FROM Product");
}
```

or ...

```csharp
using (var db = new Database()) {
    return db.Query<Product>("SELECT * FROM Product");
}
```

If you use Generic List, it will get all matching your class's properties or fields
It's all by convention
ex.
you have table structure and class like below
```sql
CREATE TABLE Category
(
    [Id] VARCHAR(30),
    [Desc] VARCHAR(30)
)

CREATE TABLE Product
(
    [Id] VARCHAR(30),
    [Name] VARCHAR(100),
    CategoryId VARCHAR(30),
    Price DECIMAL
)
```

```csharp
public class Category {
    public String Id { get; set; }
    public String Desc { get; set; }
}

public class Product {
    public String Id { get; set; }
    public String Name { get; set; }
    public String CategoryId { get; set; }
    public Decimal Price { get; set; }
}
```

so it will get Id, Name and CategoryId based on class properties.

You can do like this too.
```csharp
public class ProductViewModel {
    public String Id { get; set; }
    public String Name { get; set; }
    public String CategoryJ { get; set; }
}

using (var db = new Database()) {
    var query = new StringBuilder()
        .Append("SELECT p.[Id], p.[Name], c.[Desc] CategoryJ ")
        .Append("FROM Product p JOIN Category c ON c.[Id] = p.CategoryId ")
        .Append("WHERE p.[Id] = @0")
        .ToString();

    return db.QuerySingle<ProductViewModel>(query, prodId);
```



### 3. Execute Insert or Update

If you see above. you will get the idea quickly
```csharp
String newCompanyId =  KeyBlaster.BuildSimpleKey(8, Keywielder.KeyBlaster.SimpleKeyType.ALPHANUMERIC);
db.Execute("INSERT INTO Company VALUES (@0, @1, @2, @3, @4, @5)",
    newCompanyId, txtName.Text, txtAddress.Text, txtCity.Text, txtPhoneNo.Text, txtCEOId.Text);
```

or ...

```csharp
var query = new StringBuilder()
    .Append("UPDATE Company  ")
    .Append("SET companyName = @0, companyAddress = @1, ")
    .Append("city = @2, phone = @3, companyCEOId = @4 ")
    .Append("WHERE companyId = @5")
    .ToString();

db.Execute(query, txtName.Text, txtAddress.Text, txtCity.Text, txtPhoneNo.Text, txtCEOId.Text, txtCompanyId.Text);
```



### 4. Using named param instead

If you have ViewModel or say an object that hold your data to save, you just could drop it in like this
```csharp
var query = new StringBuilder()
    .Append("UPDATE Company  ")
    .Append("SET companyName = @Name, companyAddress = @Address, ")
    .Append("city = @City, phone = @PhoneNo, companyCEOId = @CEOId ")
    .Append("WHERE companyId = @CompanyId")
    .ToString();

db.Execute(query,
    new {
        Name = txtName.Text,
        Address = txtAddress.Text,
        City = txtCity.Text,
        PhoneNo = txtPhoneNo.Text,
        CEOId = txtCEOId.Text,
        CompanyId = txtCompanyId.Text
    });
```



## Nvy

Simple implementation of Name-Value pair object



## Kayo

Utility js library



### Issues

There are two different libs, `kayo` and `ayumi`. Currently used for Deno app which act as modules. But the modules are being setup using git submodules and Deno workspace. But seems `kayo`'s `deno.json` and `ayumi`'s `deno.json` files are outputting warning about parent workspace as well as `kayo.test` and `ayumi.test` cannot be tested because of the parent workspace.

Maybe I can read how these projects implement theirs:
- [nesterow/tailored: Isomorphic utilities, components and hooks for Fresh and Preact.](https://github.com/nesterow/tailored)
- [stephenmelnicki/denopaste: A simple paste service built with Deno 🦕 and Fresh 🍋](https://github.com/stephenmelnicki/denopaste)


---


> ❗ Note
> 
> Old documentation

### GetType

**<ins>Code</ins>**

```javascript
var Kayo = require("kayo.js");

console.log(Kayo.GetType(1));
console.log(Kayo.GetType(15.7));
console.log(Kayo.GetType(NaN));
console.log(Kayo.GetType("heyy hoo"));
console.log(Kayo.GetType({}));
console.log(Kayo.GetType(function () {}));
```


**<ins>Output</ins>**

```shell
Number  
Number  
Number  
String  
Object  
Function  
```



### Extend

**<ins>Code</ins>**

```javascript
var Kayo = require("kayo.js");

var original = {
    Name: "Original",
    Func: function () {
        console.log("original function");
    }
};

var extended = Kayo.Extend(original, {
    NewName: "Extended",
    NewFunc: function () {
        console.log("new function");
    }
});

extended.NewFunc();
console.log(extended);
```


**<ins>Output</ins>**

```shell
new function                                                                                                                               
{ Name: 'Original',                                                                                                                        
  Func: [Function],                                                                                                                        
  NewName: 'Extended',                                                                                                                     
  NewFunc: [Function] }
```



### InvalidOperationException

**<ins>Code</ins>**

```javascript
var Kayo = require("kayo.js");

try {
    throw new Kayo.InvalidOperationException("this is a custom error.");
}
catch (ex) {
    console.log(ex.message);
}
```


**<ins>Output</ins>**

```shell
InvalidOperationException: this is a custom error.
```



### ViewData

**<ins>Code</ins>**

```javascript
var ViewData = require("kayo.js").ViewData;

ViewData.Add("ProcessId", "2016070700005");
console.log(ViewData.Get("ProcessId"));
```


**<ins>Output</ins>**

2016070700005



### Hook

**<ins>Code</ins>**

```javascript
var Hook = require("kayo.js").Hook;

Hook.Add("GetName", function (name) {
    console.log("heyy hoo " + name);
});
Hook.Add("GetProcessId", function () {
    console.log("2016070900427");
});

Hook.Run("GetName", ["Jack"]);
Hook.Run("GetProcessId");
```


**<ins>Output</ins>**

```shell
heyy hoo Jack                                                                                                                              
2016070900427
```
