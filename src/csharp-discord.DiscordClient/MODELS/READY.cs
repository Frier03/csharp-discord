public class Ready
{
    public string t { get; set; }
    public int s { get; set; }
    public int op { get; set; }
    public Data d { get; set; }

    public class Data
    {
        public int V { get; set; }
        public GuildData[] guilds { get; set; }
        public RelationshipData[] relationships { get; set; }
        public UserData[] PrivateChannels { get; set; }
        public GuildIntegrationsData GuildIntegrations { get; set; }
        public UserData user { get; set; }
        public int? ReadState { get; set; }
        public int? UserGuildSettings { get; set; }
        public UserData[] ReadStates { get; set; }
    }

    public class GuildData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public bool? Owner { get; set; }
        public int? Permissions { get; set; }
        public int? MemberCount { get; set; }
        public int? Large { get; set; }
        public string[] Channels { get; set; }
        public string[] Presences { get; set; }
        public int? MaxPresences { get; set; }
        public int? MaxMembers { get; set; }
        public string VanityUrlCode { get; set; }
        public string Description { get; set; }
        public GuildWidgetData Widget { get; set; }
        public string[] Emojis { get; set; }
        public FeatureData[] Features { get; set; }
        public int? PremiumTier { get; set; }
        public int? PremiumSubscriptionCount { get; set; }
    }

    public class GuildWidgetData
    {
        public bool? Enabled { get; set; }
        public string ChannelId { get; set; }
    }

    public class FeatureData
    {
        public string Name { get; set; }
        public bool? Enabled { get; set; }
    }

    public class RelationshipData
    {
        public string id { get; set; }
        public string userid { get; set; }
        public int type { get; set; }
        public UserData user { get; set; }
    }

    public class UserData
    {
        public string id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public string avatar { get; set; }
        public bool? bot { get; set; }
        public bool? mfaenabled { get; set; }
        public string locale { get; set; }
        public bool? verified { get; set; }
        public string email { get; set; }
        public int? flags { get; set; }
        public int? premiumType { get; set; }
        public string publicFlag { get; set; }
    }
    public class Features
    {
        public string Name { get; set; }
        public bool? Enabled { get; set; }
    }

    public class GuildIntegrationsData
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public UserData Account { get; set; }
        public int? SyncedAt { get; set; }
        public bool? Enabled { get; set; }
        public bool? ExpireBehavior { get; set; }
        public int? ExpireGracePeriod { get; set; }
        public bool? User { get; set; }
        public bool? Role { get; set; }
        public int? EnableEmoticons { get; set; }
    }

    public class UserSettings
    {
        public bool? DetectPlatformAccounts { get; set; }
        public int? AnimateStickers { get; set; }
        public bool? InlineAttachmentMedia { get; set; }
        public string Status { get; set; }
        public bool? MessageDisplayCompact { get; set; }
        public bool? ViewNsfwGuilds { get; set; }
        public int? TimezoneOffset { get; set; }
        public bool? EnableTtsCommand { get; set; }
        public bool? DisableGamesTab { get; set; }
        public bool? StreamNotificationsEnabled { get; set; }
        public bool? AnimateEmoji { get; set; }
        public GuildFolder[] GuildFolders { get; set; }
        public int? RestrictedGuilds { get; set; }
        public int? FriendSourceFlags { get; set; }
        public bool? ShowCurrentGame { get; set; }
        public int? TimeFormat { get; set; }
        public bool? LocaleInfo { get; set; }
        public int? Theme { get; set; }
        public string GuildPositions { get; set; }
        public int? RestrictedChannels { get; set; }
    }

    public class GuildFolder
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string[] GuildIds { get; set; }
        public string Color { get; set; }
    }
}