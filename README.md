# Discord Webhook Wrapper

A simple, lightweight C# wrapper for sending messages and embeds to Discord webhooks without requiring external libraries like `Newtonsoft.Json`.

## Features

- Send plain text messages
- Create and send embedded messages with customizable fields, colors, timestamps, footers, and images
- Simple API, ideal for use in applications, bots, and tools

## Getting Started

### Prerequisites

- .NET 5.0 or later
- A valid Discord webhook URL


### Installation
**Visual Studio**
1. Go to the [Releases](https://github.com/xXExilXx/DiscordWebhookWrapper/releases) page.
2. Download the latest `.dll` file.
3. In your Visual Studio project, right-click on **References** > **Add Reference...** and select the downloaded `.dll` file.
**Unity**
1. In your Unity project, create a folder called **Plugins**.
2. Download the latest `.dll` file from the [Releases](https://github.com/xXExilXx/DiscordWebhookWrapper/releases) page.
3. Then drag the `.dll` into the **Plugins** folder.
### Usage

#### Initializing the Webhook Client

```csharp
var webhookUrl = "YOUR_DISCORD_WEBHOOK_URL";
var client = new DiscordWebhookClient(webhookUrl);
```

#### Sending a Plain Text Message

```csharp
await client.SendMessageAsync("Hello from the Discord Webhook Wrapper!");
```

#### Sending an Embedded Message

```csharp
var embed = new DiscordEmbed
{
    Title = "Sample Embed",
    Description = "This is an example embed message.",
    Color = 0x00FF00, // Green color
    Timestamp = DateTime.UtcNow,
    Footer = new EmbedFooter { Text = "Powered by Discord Webhook Wrapper" },
    Image = new EmbedImage { Url = "https://example.com/image.png" },
    Thumbnail = new EmbedThumbnail { Url = "https://example.com/thumbnail.png" }
};

// Add a field to the embed
embed.Fields.Add(new EmbedField { Name = "Field 1", Value = "This is a field.", Inline = true });

await client.SendEmbedAsync(embed);
```

## Embed Structure

Each `DiscordEmbed` consists of:

- **Title**: Embed title (string)
- **Description**: Embed description (string)
- **Color**: Integer color code (e.g., `0xFFFFFF` for white)
- **Fields**: List of fields (each field has a name, value, and inline property)
- **Footer**: Footer object with text and optional icon URL
- **Image**: Image object with a URL
- **Thumbnail**: Thumbnail object with a URL
- **Timestamp**: Embed timestamp (DateTime)

## Contributions

Feel free to contribute by creating issues or pull requests to improve functionality.

## License

This project is licensed under the Apache-2.0 license. See [LICENSE](LICENSE) for details.
