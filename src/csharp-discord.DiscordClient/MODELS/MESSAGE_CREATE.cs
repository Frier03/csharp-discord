using System;

public class Message
{
    public string t { get; set; }
    public int s { get; set; }
    public int op { get; set; }
    public Data d { get; set; }
}

public class Data
{
    public int type { get; set; }
    public bool tts { get; set; }
    public string timestamp { get; set; }
    public object referenced_message { get; set; }
    public bool pinned { get; set; }
    public string nonce { get; set; }
    public List<object> mentions { get; set; }
    public List<object> mention_roles { get; set; }
    public bool mention_everyone { get; set; }
    public Member member { get; set; }
    public string id { get; set; }
    public int flags { get; set; }
    public List<object> embeds { get; set; }
    public object edited_timestamp { get; set; }
    public string content { get; set; }
    public List<object> components { get; set; }
    public string channel_id { get; set; }
    public Author author { get; set; }
    public List<object> attachments { get; set; }
    public string guild_id { get; set; }
}

public class Member
{
    public List<string> roles { get; set; }
    public object premium_since { get; set; }
    public bool pending { get; set; }
    public string nick { get; set; }
    public bool mute { get; set; }
    public string joined_at { get; set; }
    public int flags { get; set; }
    public bool deaf { get; set; }
    public object communication_disabled_until { get; set; }
    public object avatar { get; set; }
}

public class Author
{
    public string username { get; set; }
    public int public_flags { get; set; }
    public string id { get; set; }
    public string discriminator { get; set; }
    public object avatar_decoration { get; set; }
    public string avatar { get; set; }
}
