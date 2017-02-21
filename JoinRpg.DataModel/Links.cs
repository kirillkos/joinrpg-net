﻿namespace JoinRpg.DataModel
{
  public enum LinkType
  {
    ResultUser,
    ResultCharacterGroup,
    ResultCharacter,
    Claim,
    Plot,
    Comment,
    Project,
    CommentDiscussion
  }

  public interface ILinkable
  {
    LinkType LinkType { get; }
    string Identification { get; }
    int? ProjectId { get; }
  }
}
