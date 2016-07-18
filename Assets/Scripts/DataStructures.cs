using UnityEngine;
using System.Collections;

public static class DataStructures {
    public static Emote[] childishResponses = new Emote[] { Emote.neutral, Emote.happy, Emote.heart, Emote.sad, Emote.wow, Emote.angry };
    public static Emote[] lovingResponses = new Emote[] { Emote.neutral, Emote.angry, Emote.happy, Emote.wow, Emote.heart, Emote.sad };
    public static Emote[] tsundereResponses = new Emote[] { Emote.neutral, Emote.wow, Emote.heart, Emote.angry, Emote.happy, Emote.sad };
}

public enum PlayerType { unassigned, childish, loving, tsundere };
public enum Emote { neutral, happy, sad, wow, angry, heart };
public enum Relationship { mortal, rival, acquaintance, friend, love, soulmate }
