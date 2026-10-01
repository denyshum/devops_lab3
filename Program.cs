// Встановлення кодування для коректного відображення української мови в консолі
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Створення виконавця
Person artist = new Person("Святослав", "Вакарчук", new DateTime(1975, 5, 14));

// Крок 1: створити об'єкт Album, вивести через ToShortString()
Album album = new Album("Земля", artist, Genre.Rock, Array.Empty<Song>());
Console.WriteLine("Крок 1: Створення альбому");
Console.WriteLine(album.ToShortString());

// Крок 2: перевірити індексатор для Rock, Pop, Jazz
Console.WriteLine("\nКрок 2: Перевірка індексатора");
Console.WriteLine($"Чи є цей альбом у жанрі Rock? {album[Genre.Rock]}");
Console.WriteLine($"Чи є цей альбом у жанрі Pop? {album[Genre.Pop]}");
Console.WriteLine($"Чи є цей альбом у жанрі Jazz? {album[Genre.Jazz]}");

// Крок 3: оновити властивості, вивести через ToString()
Console.WriteLine("\nКрок 3: Оновлення властивостей");
album.Title = "Вночі"; // Оновлюємо назву
album.AlbumGenre = Genre.Jazz; // Оновлюємо жанр
Console.WriteLine(album.ToString());

// Крок 4: додати пісні через AddSongs(), вивести ToString()
Console.WriteLine("\nКрок 4: Додавання пісень");
Song s1 = new Song("Така, як ти", 224, new DateTime(2008, 2, 14));
Song s2 = new Song("Неоднаково", 285, new DateTime(2008, 3, 10));

// Додаємо створені вище пісні та ще одну нову безпосередньо в метод
album.AddSongs(s1, s2, new Song("Вночі", 252, new DateTime(2008, 4, 1)));
Console.WriteLine(album.ToString());
Console.WriteLine($"\nЗагальна тривалість у хвилинах: {album.TotalDurationMinutes:F2} хв.");

// Визначення класу Person згідно з вимогами
public class Person
{
    // Закриті поля
    private string _name;
    private string _surname;
    private DateTime _birthDate;

    // Конструктор з трьома параметрами для ініціалізації всіх полів
    public Person(string name, string surname, DateTime birthDate)
    {
        this._name = name;
        this._surname = surname;
        this._birthDate = birthDate;
    }

    // Конструктор без параметрів, що ініціалізує поля значеннями за замовчуванням
    public Person() : this("Unknown", "Unknown", new DateTime(2000, 1, 1))
    {

    }

    // Властивість для доступу до поля імені
    public string Name
    {
        get
        {
            return this._name;
        }
        set
        {
            this._name = value;
        }
    }

    // Властивість для доступу до поля прізвища
    public string Surname
    {
        get
        {
            return this._surname;
        }
        set
        {
            this._surname = value;
        }
    }

    // Властивість для доступу до дати народження
    public DateTime BirthDate
    {
        get
        {
            return this._birthDate;
        }
        set
        {
            this._birthDate = value;
        }
    }

    // Властивість для отримання та зміни лише року народження
    public int BirthYear
    {
        get
        {
            return this._birthDate.Year;
        }
        set
        {
            this._birthDate = this._birthDate.AddYears(value - _birthDate.Year);
        }
    }

    // Переобтяжений метод для формування рядка з усіма полями
    public override string ToString()
    {
        return $"Ім'я: {_name}, Прізвище: {_surname}, Дата народження: {_birthDate.ToShortDateString()}";
    }

    // Віртуальний метод, що повертає лише ім'я та прізвище
    public virtual string ToShortString()
    {
        return $"{_name} {_surname}";
    }
}

// Перерахування музичних жанрів
public enum Genre
{
    Rock,
    Pop,
    Jazz

}

// Клас, що описує пісню
public class Song
{
    // Відкриті автоматичні властивості
    public string Title { get; set; }
    public int DurationSeconds { get; set; }
    public DateTime RecordDate { get; set; }

    // Конструктор з параметрами
    public Song(string title, int durationSeconds, DateTime recordDate)
    {
        Title = title;
        DurationSeconds = durationSeconds;
        RecordDate = recordDate;
    }

    // Конструктор без параметрів
    public Song() : this("Unknown song", 0, DateTime.Now)
    {

    }

    // Переобтяжений метод для зручного виводу інформації про пісню
    public override string ToString()
    {
        return $"Пісня: '{Title}' ({DurationSeconds} сек.), Записана: {RecordDate.ToShortDateString()}";
    }
}

// Клас, що описує музичний альбом
public class Album
{
    // Закриті поля
    private string _title;
    private Person _artist;
    private Genre _genre;
    private Song[] _songs;

    // Конструктор з параметрами (з перевіркою масиву на null)
    public Album(string title, Person artist, Genre genre, Song[] songs)
    {
        this._title = title;
        this._artist = artist;
        this._genre = genre;
        this._songs = songs ?? Array.Empty<Song>();
    }

    // Конструктор без параметрів
    public Album() : this("Unknown Album", new Person(), Genre.Pop, Array.Empty<Song>())
    {

    }

    // Властивості get/set для доступу до закритих полів
    public string Title
    {
        get 
        { 
            return this._title;
        }
        set 
        { 
            this._title = value;
        }
    }

    public Person Artist
    {
        get 
        { 
            return this._artist;
        }
        set 
        { 
            this._artist = value;
        }
    }

    public Genre AlbumGenre
    {
        get 
        { 
            return this._genre;
        }
        set 
        { 
            this._genre = value;
        }
    }

    public Song[] Songs
    {
        get 
        { 
            return this._songs;
        }
        set { 
            this._songs = value;
        }
    }

    // Властивість тільки для читання, яка обчислює загальну тривалість альбому в хвилинах
    public double TotalDurationMinutes
    {
        get
        {
            if (_songs == null || _songs.Length == 0) return 0.0;
            int totalSeconds = 0;
            foreach (Song song in _songs)
            {
                totalSeconds += song.DurationSeconds;
            }
            return totalSeconds / 60.0;
        }
    }

    // Індексатор булевого типу з параметром Genre
    public bool this[Genre genre]
    {
        get
        {
            return this._genre == genre;
        }
    }

    // Метод для додавання пісень у масив (використовує зміну розміру масиву)
    public void AddSongs(params Song[] newSongs)
    {
        if (newSongs == null || newSongs.Length == 0) return;

        int oldLength = this._songs.Length;
        Array.Resize(ref this._songs, oldLength + newSongs.Length);
        Array.Copy(newSongs, 0, this._songs, oldLength, newSongs.Length);
    }

    // Переобтяжений ToString для детальної інформації про альбом та список його пісень
    public override string ToString()
    {
        string result = $"Альбом: '{_title}', Виконавець: [{_artist}], Жанр: {_genre}\nСписок пісень:\n";
        if (_songs != null && _songs.Length > 0)
        {
            foreach (Song song in _songs)
            {
                result += $" - {song.ToString()}\n";
            }
        }
        else
        {
            result += " - Немає пісень\n";
        }
        return result.TrimEnd(); // Забираємо зайвий перенос рядка в кінці
    }

    // Віртуальний метод для короткого виводу (з обчисленням середньої довжини пісні)
    public virtual string ToShortString()
    {
        double avgDurationSeconds = 0;
        if (_songs != null && _songs.Length > 0)
        {
            int totalSec = 0;
            foreach (Song song in _songs)
            {
                totalSec += song.DurationSeconds;
            }
            avgDurationSeconds = (double)totalSec / _songs.Length;
        }

        return $"Альбом: '{_title}', Виконавець: {_artist.ToShortString()}, Жанр: {_genre}, Сер. довжина пісні: {avgDurationSeconds:F1} сек.";
    }
}