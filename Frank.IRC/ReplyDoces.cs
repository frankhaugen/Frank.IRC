﻿/*
 *  The ircd.net project is an IRC deamon implementation for the .NET Plattform
 *  It should run on both .NET and Mono
 *
 * Copyright (c) 2009-2017, Thomas Bruderer, apophis@apophis.ch All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * * Redistributions of source code must retain the above copyright notice, this
 *   list of conditions and the following disclaimer.
 *
 * * Redistributions in binary form must reproduce the above copyright notice,
 *   this list of conditions and the following disclaimer in the documentation
 *   and/or other materials provided with the distribution.
 *
 * * Neither the name of ArithmeticParser nor the names of its
 *   contributors may be used to endorse or promote products derived from
 *   this software without specific prior written permission.
 */

/*
 * This has bee shamelessly copied from the ircd.net project on GitHub: https://github.com/FreeApophis/ircddotnet/blob/c3925aff069cb65cd2cb5809c279737882f4f576/IrcD.Net/ServerReplies/ReplyCode.cs
 */

namespace Frank.IRC;

/// <summary>
///    This enum contains all reply codes that are defined in the RFCs
/// </summary>
public enum ReplyCode
{
    Null =                           000,
    Welcome =                        001,
    YourHost =                       002,
    Created =                        003,
    MyInfo =                         004,
    // ReSharper disable once InconsistentNaming
    ISupport =                       005,
    //RFC says this is 005 - but no implementation exists - 010 is the alternative, cause 005 is used for RPL_ISUPPORT.
    Bounce =                         010,
    TraceLink =                      200,
    TraceConnecting =                201,
    TraceHandshake =                 202,
    TraceUnknown =                   203,
    TraceOperator =                  204,
    TraceUser =                      205,
    TraceServer =                    206,
    TraceService =                   207,
    TraceNewType =                   208,
    TraceClass =                     209,
    TraceReconnect =                 210,
    StatsLinkInfo =                  211,
    StatsCommands =                  212,
    EndOfStats =                     219,
    UserModeIs =                     221,
    ServiceList =                    234,
    ServiceListEnd =                 235,
    StatsUptime =                    242,
    StatsOLine =                     243,
    ListUserClient =                 251,
    ListUserOp =                     252,
    ListUserUnknown =                253,
    ListUserChannels =               254,
    ListUserMe =                     255,
    AdminMe =                        256,
    AdminLocation1 =                 257,
    AdminLocation2 =                 258,
    AdminEmail =                     259,
    TraceLog =                       261,
    TraceEnd =                       262,
    TryAgain =                       263,
    Away =                           301,
    UserHost =                       302,
    IsOn =                           303,
    UnAway =                         305,
    NowAway =                        306,
    WhoIsRegistered =                307,
    WhoIsUser =                      311,
    WhoIsServer =                    312,
    WhoIsOperator =                  313,
    WhoWasUser =                     314,
    EndOfWho =                       315,
    WhoIsIdle =                      317,
    EndOfWhoIs =                     318,
    WhoIsChannels =                  319,
    ListStart =                      321,
    List =                           322,
    ListEnd =                        323,
    ChannelModeIs =                  324,
    UniqueOpIs =                     325,
    NoTopic =                        331,
    Topic =                          332,
    Inviting =                       341,
    Summoning =                      342,
    InviteList =                     346,
    EndOfInviteList =                347,
    ExceptionList =                  348,
    EndOfExceptionList =             349,
    Version =                        351,
    WhoReply =                       352,
    NamesReply =                     353,
    Links =                          364,
    EndOfLinks =                     365,
    EndOfNames =                     366,
    BanList =                        367,
    EndOfBanList =                   368,
    EndOfWhoWas =                    369,
    Info =                           371,
    Motd =                           372,
    EndOfInfo =                      374,
    MotdStart =                      375,
    EndOfMotd =                      376,
    YouAreOper =                     381,
    Rehashing =                      382,
    YouAreService =                  383,
    Time =                           391,
    UsersStart =                     392,
    Users =                          393,
    EndOfUsers =                     394,
    NoUsers =                        395,
    ErrorNoSuchNickname =            401,
    ErrorNoSuchServer =              402,
    ErrorNoSuchChannel =             403,
    ErrorCannotSendToChannel =       404,
    ErrorTooManyChannels =           405,
    ErrorWasNoSuchNickname =         406,
    ErrorTooManyTargets =            407,
    ErrorNoSuchService =             408,
    ErrorNoOrigin =                  409,
    ErrorInvalidCapabilitesCommand = 410,
    ErrorNoRecipient =               411,
    ErrorNoTextToSend =              412,
    ErrorNoTopLevel =                413,
    ErrorWildTopLevel =              414,
    ErrorBadMask =                   415,
    ErrorUnknownCommand =            421,
    ErrorNoMotd =                    422,
    ErrorNoAdminInfo =               423,
    ErrorFileError =                 424,
    ErrorNoNicknameGiven =           431,
    ErrorErroneusNickname =          432,
    ErrorNicknameInUse =             433,
    ErrorNicknameCollision =         436,
    ErrorUnavailableResource =       437,
    ErrorUserNotInChannel =          441,
    ErrorNotOnChannel =              442,
    ErrorUserOnChannel =             443,
    ErrorNoLogin =                   444,
    ErrorSummonDisabled =            445,
    ErrorUsersDisabled =             446,
    ErrorNotRegistered =             451,
    ErrorNeedMoreParams =            461,
    ErrorAlreadyRegistered =         462,
    ErrorNoPermissionForHost =       463,
    ErrorPasswordMismatch =          464,
    ErrorYouAreBannedCreep =         465,
    ErrorYouWillBeBanned =           466,
    ErrorKeySet =                    467,
    ErrorChannelIsFull =             471,
    ErrorUnknownMode =               472,
    ErrorInviteOnlyChannel =         473,
    ErrorBannedFromChannel =         474,
    ErrorBadChannelKey =             475,
    ErrorBadChannelMask =            476,
    ErrorNoChannelModes =            477,
    ErrorBanListFull =               478,
    ErrorCannotKnock =               480,
    ErrorNoPrivileges =              481,
    ErrorChannelOpPrivilegesNeeded = 482,
    ErrorCannotKillServer =          483,
    ErrorRestricted =                484,
    ErrorUniqueOpPrivilegesNeeded =  485,
    ErrorNoOperHost =                491,
    ErrorUserModeUnknownFlag =       501,
    ErrorUsersDoNotMatch =           502,
    // kircd codes language
    YourLanguageIs =                 687,
    Language =                       688,
    WhoIsLanguage =                  690
}