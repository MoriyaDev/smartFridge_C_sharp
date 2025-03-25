# המקרר החכם - צד שרת (C# .NET)

פרויקט זה הוא צד השרת של המקרר החכם, הכולל API לניהול מוצרים, מתכונים ומשתמשים.

## טכנולוגיות
* C# .NET 7
* Entity Framework Core
* ASP.NET Web API
* JWT Authentication
* SQL Server

## התקנה והרצה
1. שחזור החבילות:
```bash
dotnet restore
```

2. הגדרת מסד הנתונים:
```bash
dotnet ef database update
```

3. הרצת השרת:
```bash
dotnet run
```

השרת יהיה זמין בכתובת `http://localhost:5000/api/`

## מבנה הפרויקט
* `Controllers/` - בקרי API לניהול מוצרים, מתכונים ומשתמשים
* `Services/` - לוגיקת המערכת
* `Repositories/` - שכבת הגישה לנתונים
* `Models/` - מודלים של האובייקטים

## תכונות עיקריות
* ניהול מוצרים (CRUD)
* חיפוש מתכונים לפי המוצרים במקרר
* הרשאות JWT לניהול משתמשים
* שילוב מסד נתונים SQL Server

## דוגמאות API
* שליפת מוצרים: `GET /api/products`
* הוספת מוצר: `POST /api/products`
* חיפוש מתכונים: `GET /api/recipes/search?ingredients=חלב,ביצים,לחם`

