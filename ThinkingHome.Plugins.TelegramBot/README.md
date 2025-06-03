*ThinkingHome.Plugins.TelegramBot*

[![NuGet Pre Release](https://img.shields.io/nuget/vpre/ThinkingHome.Plugins.TelegramBot.svg)]()

# TelegramBotPlugin

Telegram бот для умного дома. Предоставляет плагинам и сценариям возможность отправлять и обрабатывать полученные сообщения Telegram.

## Конфигурация

Для работы необходимо указать в настройках API token бота и список разрешенных логинов (usernames). Бот будет обрабатывать сообщения от логинов, перечисленных в поле `authorizedLogins` (проверка логинов не зависит от регистра).

```js
{
    "plugins": {

        ...

        "ThinkingHome.Plugins.TelegramBot.TelegramBotPlugin": {
            "token": "346298996:AAHOFLW6HipdOGUKqfWijB_3LqMHQsFpGcY", 
            "authorizedLogins": ["user1", "USER2"]
        }
    }
}
```

### Регистрация нового бота и получнеие токена

Зарегистрировать нового бота и получить его токен можно в приложении Telegram через специального бота [@BotFather](http://t.me/BotFather). Добавьте бота [@BotFather](http://t.me/BotFather) в свой список контактов и отправьте ему команду */start*, чтобы он прислал инструкцию, как с ним работать.

Чтобы зарегистрировать нового бота, отправьте боту [@BotFather](http://t.me/BotFather) команду */newbot*. [@BotFather](http://t.me/BotFather) задаст несколько вопросов о новом боте и пришлет его API token.

## API

### `void SendMessage(ChatId chatIdOrLogin, string text)`

Отправляет сообщение в указанный чат.

#### Параметры

- `ChatId chatIdOrLogin` - ID чата (число) или логин (строка, начинающаяся с `@`).
- `string text` - текст сообщения.

#### Пример

```csharp
public class MyPlugin : PluginBase
{
    private readonly TelegramBotPlugin telegram;
    
    private void MyMethod()
    {
        telegram.SendMessage("@user1", "Привет!");
    }
}
```

### `void SendPhoto(ChatId chatIdOrLogin, string filename, Stream content)`

Отправляет изображение в указанный чат.

#### Параметры

- `ChatId chatIdOrLogin` - ID чата (число) или логин (строка, начинающаяся с `@`).
- `string filename` - имя файла с изображением (который можно скачать).
- `Stream content` - содержимое файла с изображением.

#### Пример

```csharp
public class MyPlugin : PluginBase
{
    private readonly TelegramBotPlugin telegram;
    
    private void MyMethod()
    {
        using (var stream = System.IO.File.OpenRead("/Users/username/photo.jpg"))
        {
            telegram.SendPhoto("@user1", "photo.jpg", stream);
        }
    }
}
```

### `void SendPhoto(ChatId chatIdOrLogin, Uri url)`

Отправляет в указанный чат изображение, расположенное по заданному URL.

#### Параметры

- `ChatId chatIdOrLogin` - ID чата (число) или логин (строка, начинающаяся с `@`).
- `Uri url` - URL изображения.

#### Пример

```csharp
public class MyPlugin : PluginBase
{
    private readonly TelegramBotPlugin telegram;
    
    private void MyMethod()
    {        
        telegram.SendPhoto("@user1", new Uri("http://example.com/images/котики.jpg"));
    }
}
```

### `void SendFile(ChatId chatIdOrLogin, string filename, Stream content)`

Отправляет файл в указанный чат.

#### Параметры

- `ChatId chatIdOrLogin` - ID чата (число) или логин (строка, начинающаяся с `@`).
- `string filename` - имя файла.
- `Stream content` - содержимое файла.

#### Пример

```csharp
public class MyPlugin : PluginBase
{
    private readonly TelegramBotPlugin telegram;
    
    private void MyMethod()
    {
        using (var stream = System.IO.File.OpenRead("/Users/username/user-manual.pdf"))
        {
            telegram.SendFile("@user1", "user-manual.pdf", stream);
        }
    }
}
```

### `void SendFile(ChatId chatIdOrLogin, Uri url)`

Отправляет в указанный чат файл, расположенный по заданному URL.

#### Параметры

- `ChatId chatIdOrLogin` - ID чата (число) или логин (строка, начинающаяся с `@`).
- `Uri url` - URL файла.

#### Пример

```csharp
public class MyPlugin : PluginBase
{
    private readonly TelegramBotPlugin telegram;
    
    private void MyMethod()
    {
        telegram.SendFile("@user1", new Uri("http://example.com/user-manual.pdf"));
    }
}
```

### `[TelegramMessageHandler]`

Вы можете отметить методы своего плагина атрибутом `ThinkingHome.Plugins.TelegramBot.TelegramMessageHandlerAttribute`. Метод вашего плагина будет автоматически вызываться при получении сообщений через Telegram.

В качестве параметра атрибута необходимо указать команду бота, которую следует обрабатывать. Например, если указать значение `"test"`, то ваш метод будет вызван для всех сообщений, котрые начинаются с `/test`.

Команды бота могут содержать английские и русские буквы, цифры, символы "дефис" `-` и "подчеркивание" `_`. В начале команды должен быть слэш `/`.

Если параметр атрибута не указан, метод будет вызываться для всех сообщений. Чтобы обрабатывать только сообщения, которые в тексте не содержат ни одной команды, передайте в качестве значения параметра пустую строку.

Сигнатура метода должна соответствовать делегату `ThinkingHome.Plugins.TelegramBot.TelegramMessageHandlerDelegate`:

```csharp
public delegate void TelegramMessageHandlerDelegate(string command, Message message);
```

#### Параметры

- `string command` - команда, с которой начинается сообщение.
- `Telegram.Bot.Types.Message message` - полученное сообщение.

#### Пример

```csharp
// обработка сообщений, которые начинаются с команды 'test'
[TelegramMessageHandler("test")]
public void TestMessageHandler1(string command, Message msg)
{
    // command == "test"

    Logger.LogInformation($"Получено новое сообщение: {msg.Text}");
}

// обработка сообщения, которое не содержит команду
[TelegramMessageHandler("")]
public void TestMessageHandler2(string command, Message msg)
{
    // command == ""

    Logger.LogInformation($"Получено новое сообщение: {msg.Text}");
}

// обработка всех сообщений
[TelegramMessageHandler]
public void TestMessageHandler3(string command, Message msg)
{
    switch (command)
    {
        case: "test":
            Logger.LogInformation("Tested");
            break;
        case: "say-hello":
            Logger.LogInformation("Hello!");
            break;
        default:
            Logger.LogInformation("Unknown command");
            break;
    }
}

```
