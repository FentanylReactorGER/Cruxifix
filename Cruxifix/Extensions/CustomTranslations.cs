using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Cruxifix.Extensions;

public class CustomTranslations
{
    public Dictionary<RoleTypeId, string> RoleTranslations { get; set; } = new();
    public Dictionary<Team, string> TeamTranslations { get; set; } = new();
    public Dictionary<ItemType, string> ItemTranslations { get; set; } = new();
    public Dictionary<RoomType, string> RoomTranslations { get; set; } = new();
    public Dictionary<ZoneType, string> ZoneTranslations { get; set; } = new();
    public Dictionary<RoleTypeId, string> AbilityRoleTranslation { get; set; } = new();

    private static readonly string FilePath = Path.Combine(Paths.Plugins, "CustomRoleTranslation.yaml");

    public void LoadOrCreate()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

        if (File.Exists(FilePath))
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var yaml = File.ReadAllText(FilePath);
            var loaded = deserializer.Deserialize<CustomTranslations>(yaml);

            RoleTranslations = loaded.RoleTranslations;
            TeamTranslations = loaded.TeamTranslations;
            ItemTranslations = loaded.ItemTranslations;
            RoomTranslations = loaded.RoomTranslations;
            AbilityRoleTranslation = loaded.AbilityRoleTranslation;
        }
        else
        {
            RoleTranslations = new()
            {
                { RoleTypeId.None, "No Role" },
                { RoleTypeId.Spectator, "Spectator" },
                { RoleTypeId.ClassD, "Class-D" },
                { RoleTypeId.Scientist, "Scientist" },
                { RoleTypeId.FacilityGuard, "Facility Guard" },
                { RoleTypeId.NtfPrivate, "MTF - Private" },
                { RoleTypeId.NtfSergeant, "MTF - Sergeant" },
                { RoleTypeId.NtfCaptain, "MTF - Captain" },
                { RoleTypeId.NtfSpecialist, "MTF - Specialist" },
                { RoleTypeId.ChaosConscript, "Chaos - Conscript" },
                { RoleTypeId.ChaosRifleman, "Chaos - Rifleman" },
                { RoleTypeId.ChaosRepressor, "Chaos - Repressor" },
                { RoleTypeId.ChaosMarauder, "Chaos - Marauder" },
                { RoleTypeId.Scp049, "SCP-049" },
                { RoleTypeId.Scp0492, "SCP-049-2" },
                { RoleTypeId.Scp079, "SCP-079" },
                { RoleTypeId.Scp096, "SCP-096" },
                { RoleTypeId.Scp106, "SCP-106" },
                { RoleTypeId.Scp173, "SCP-173" },
                { RoleTypeId.Scp939, "SCP-939" },
                { RoleTypeId.Tutorial, "Tutorial" },
                { RoleTypeId.Overwatch, "Overwatch Commander" },
                { RoleTypeId.Destroyed, "Destroyed" }
            };

            TeamTranslations = new()
            {
                { Team.Dead, "Dead" },
                { Team.SCPs, "SCPs" },
                { Team.Scientists, "Scientists" },
                { Team.FoundationForces, "MTF" },
                { Team.ChaosInsurgency, "Chaos Insurgency" },
                { Team.OtherAlive, "Other" },
                { Team.ClassD, "Class-D" },
            };

            ZoneTranslations = new()
            {
                { ZoneType.Entrance, "Entrance Zone" },
                { ZoneType.HeavyContainment, "Heavy Containment Zone" },
                { ZoneType.LightContainment, "Light Containment Zone" },
                { ZoneType.Pocket, "Pocket Dimension" },
                { ZoneType.Surface, "Surface" },
                { ZoneType.Other, "Other" },
                { ZoneType.Unspecified, "Unspecified" },
            };
            
            AbilityRoleTranslation = new()
            {
                { RoleTypeId.Scp049, "SCP-049" },
                { RoleTypeId.Scp0492, "SCP-049-2" },
                { RoleTypeId.Scp079, "SCP-079" },
                { RoleTypeId.Scp096, "SCP-096" },
                { RoleTypeId.Scp106, "SCP-106" },
                { RoleTypeId.Scp173, "SCP-173" },
                { RoleTypeId.Scp939, "SCP-939" },
            };

            ItemTranslations = new()
            {
                { ItemType.Radio, "Radio" },
                { ItemType.Painkillers, "Painkillers" },
                { ItemType.GunCOM15, "Pistol COM15" },
                { ItemType.Coin, "Coin" },
                { ItemType.GunCOM18, "Pistol COM18" },
                { ItemType.GunFSP9, "SMG FSP9" },
                { ItemType.GunCrossvec, "SMG Crossvec" },
                { ItemType.GunE11SR, "Rifle E11-SR" },
                { ItemType.GunShotgun, "Shotgun" },
                { ItemType.GunLogicer, "LMG Logicer" },
                { ItemType.ParticleDisruptor, "Particle Disruptor" },
                { ItemType.Flashlight, "Flashlight" },
                { ItemType.GrenadeFlash, "Flashbang" },
                { ItemType.GrenadeHE, "Grenade" },
                { ItemType.KeycardChaosInsurgency, "Chaos Keycard" },
                { ItemType.Medkit, "Medkit" },
                { ItemType.Adrenaline, "Adrenaline" },
                { ItemType.SCP500, "SCP-500" },
                { ItemType.SCP207, "SCP-207" },
                { ItemType.SCP2176, "SCP-2176" },
                { ItemType.SCP1576, "SCP-1576" },
                { ItemType.SCP268, "SCP-268" },
                { ItemType.SCP1853, "SCP-1853" },
                { ItemType.Jailbird, "Jailbird" },
                { ItemType.KeycardJanitor, "Janitor Keycard" },
                { ItemType.KeycardScientist, "Scientist Keycard" },
                { ItemType.KeycardResearchCoordinator, "Research Coordinator Keycard" },
                { ItemType.KeycardZoneManager, "Zone Manager Keycard" },
                { ItemType.KeycardGuard, "Guard Keycard" },
                { ItemType.KeycardContainmentEngineer, "Containment Engineer Keycard" },
                { ItemType.KeycardMTFOperative, "MTF Operative Keycard" },
                { ItemType.KeycardMTFCaptain, "MTF Captain Keycard" },
                { ItemType.KeycardFacilityManager, "Facility Manager Keycard" },
                { ItemType.KeycardO5, "O5 Keycard" }
            };

            RoomTranslations = new()
            {
                { RoomType.LczClassDSpawn, "LCZ - Class-D Spawn" },
                { RoomType.LczCafe, "LCZ - Cafeteria" },
                { RoomType.Lcz330, "LCZ - SCP-330" },
                { RoomType.Lcz173, "LCZ - SCP-173" },
                { RoomType.LczCrossing, "LCZ - Crossing" },
                { RoomType.LczStraight, "LCZ - Hallway" },
                { RoomType.LczTCross, "LCZ - T-Cross" },
                { RoomType.LczArmory, "LCZ - Armory" },
                { RoomType.Lcz914, "LCZ - SCP-914" },
                { RoomType.LczGlassBox, "LCZ - Glass Room" },
                { RoomType.LczToilets, "LCZ - Toilets" },
                { RoomType.LczCheckpointA, "LCZ - Checkpoint A" },
                { RoomType.LczCheckpointB, "LCZ - Checkpoint B" },
                { RoomType.Hcz049, "HCZ - SCP-049" },
                { RoomType.Hcz079, "HCZ - SCP-079" },
                { RoomType.Hcz096, "HCZ - SCP-096" },
                { RoomType.Hcz106, "HCZ - SCP-106" },
                { RoomType.HczArmory, "HCZ - Armory" },
                { RoomType.HczNuke, "HCZ - Nuke Room" },
                { RoomType.HczCrossRoomWater, "HCZ - Water Treatment" },
                { RoomType.HczTesla, "HCZ - Tesla" },
                { RoomType.HczStraight, "HCZ - Hallway" },
                { RoomType.HczStraightC, "HCZ - Hallway" },
                { RoomType.HczStraightVariant, "HCZ - Hallway" },
                { RoomType.HczStraightPipeRoom, "HCZ - Pipe Room" },
                { RoomType.HczEzCheckpointA, "HCZ - EZ Checkpoint A" },
                { RoomType.HczEzCheckpointB, "HCZ - EZ Checkpoint B" },
                { RoomType.HczCrossing, "HCZ - Crossing" },
                { RoomType.HczCurve, "HCZ - Curve" },
                { RoomType.HczIntersection, "HCZ - T-Intersection" },
                { RoomType.EzCollapsedTunnel, "EZ - Collapsed Tunnel" },
                { RoomType.EzGateA, "EZ - Gate A" },
                { RoomType.EzGateB, "EZ - Gate B" },
                { RoomType.EzStraight, "EZ - Hallway" },
                { RoomType.EzStraightColumn, "EZ - Hallway Column" },
                { RoomType.EzChef, "EZ - Offices" },
                { RoomType.EzCurve, "EZ - Curve" },
                { RoomType.EzTCross, "EZ - T-Cross" },
                { RoomType.EzCrossing, "EZ - Crossing" },
                { RoomType.EzIntercom, "EZ - Intercom" },
                { RoomType.EzDownstairsPcs, "EZ - PCs (Lower)" },
                { RoomType.EzUpstairsPcs, "EZ - PCs (Upper)" },
                { RoomType.EzShelter, "EZ - Shelter" },
                { RoomType.EzVent, "EZ - Vent" },
                { RoomType.Surface, "Surface" }
            };

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = serializer.Serialize(this);
            File.WriteAllText(FilePath, yaml);
        }
    }

    public string GetRoleTranslation(RoleTypeId roleType) => RoleTranslations.TryGetValue(roleType, out var value) ? value : roleType.ToString();
    public string GetRoomTranslation(RoomType roomType) => RoomTranslations.TryGetValue(roomType, out var value) ? value : roomType.ToString();
    public string GetItemTranslation(ItemType itemType) => ItemTranslations.TryGetValue(itemType, out var value) ? value : itemType.ToString();
    public string GetTeamTranslation(Team team) => TeamTranslations.TryGetValue(team, out var value) ? value : team.ToString();
    
    public string GetZoneTranslation(ZoneType zoneType) => ZoneTranslations.TryGetValue(zoneType, out var value) ? value : zoneType.ToString();
    public string GetAbilityRoleTranslation(RoleTypeId roleType) => AbilityRoleTranslation.TryGetValue(roleType, out var value) ? value : roleType.ToString();
}
