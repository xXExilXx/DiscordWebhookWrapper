using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordWebhookWrapper
{
    public class DiscordEmbed
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int Color { get; set; } = 0xFFFFFF;
        public List<EmbedField> Fields { get; set; } = new List<EmbedField>();
        public EmbedFooter Footer { get; set; } = new EmbedFooter();
        public EmbedImage Image { get; set; } = new EmbedImage();
        public EmbedThumbnail Thumbnail { get; set; } = new EmbedThumbnail();
        public DateTime? Timestamp { get; set; }
    }

    public class EmbedField
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool Inline { get; set; }
    }

    public class EmbedFooter
    {
        public string Text { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }

    public class EmbedImage
    {
        public string Url { get; set; } = string.Empty;
    }

    public class EmbedThumbnail
    {
        public string Url { get; set; } = string.Empty;
    }



    public class DiscordEmbedBuilder
    {
        private readonly DiscordEmbed _embed = new DiscordEmbed();

        public DiscordEmbedBuilder WithTitle(string title)
        {
            _embed.Title = title;
            return this;
        }

        public DiscordEmbedBuilder WithDescription(string description)
        {
            _embed.Description = description;
            return this;
        }

        public DiscordEmbedBuilder WithUrl(string url)
        {
            _embed.Url = url;
            return this;
        }

        public DiscordEmbedBuilder WithColor(int color)
        {
            _embed.Color = color;
            return this;
        }

        public DiscordEmbedBuilder AddField(string name, string value, bool inline = false)
        {
            _embed.Fields.Add(new EmbedField { Name = name, Value = value, Inline = inline });
            return this;
        }

        public DiscordEmbedBuilder WithFooter(string text, string? iconUrl = null)
        {
            _embed.Footer = new EmbedFooter { Text = text, IconUrl = iconUrl };
            return this;
        }

        public DiscordEmbedBuilder WithImage(string url)
        {
            _embed.Image = new EmbedImage { Url = url };
            return this;
        }

        public DiscordEmbedBuilder WithThumbnail(string url)
        {
            _embed.Thumbnail = new EmbedThumbnail { Url = url };
            return this;
        }

        public DiscordEmbedBuilder WithTimestamp(DateTime? timestamp = null)
        {
            _embed.Timestamp = timestamp ?? DateTime.UtcNow;
            return this;
        }

        public DiscordEmbed Build()
        {
            return _embed;
        }
    }

    public class DiscordWebhookClient
    {
        private readonly string _webhookUrl;
        private static readonly HttpClient _httpClient = new HttpClient();

        public DiscordWebhookClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task SendMessageAsync(string content, string? username = null, string? avatarUrl = null, List<DiscordEmbed>? embeds = null)
        {
            var payload = new
            {
                content,
                username,
                avatar_url = avatarUrl,
                embeds
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var contentMessage = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_webhookUrl, contentMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send message. Status code: {response.StatusCode}");
            }
        }
    }
}
