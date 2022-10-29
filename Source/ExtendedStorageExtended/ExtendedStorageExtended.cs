using System.Linq;
using RimWorld;
using Verse;

namespace ExtendedStorageExtended;

[StaticConstructorOnStartup]
public class ExtendedStorageExtended
{
    static ExtendedStorageExtended()
    {
        var allWeapons = ThingCategoryDefOf.Weapons.DescendantThingDefs.Where(def =>
            !def.thingCategories.Contains(ThingCategoryDef.Named("Grenades")) &&
            !def.thingCategories.Contains(ThingCategoryDef.Named("MortarShells")));

        var tribalWeaponStorage = ThingDef.Named("Storage_WeaponsStack");
        var smallWeaponStorage = ThingDef.Named("Storage_SmallWeaponsRack");
        var largeWeaponStorage = ThingDef.Named("Storage_LargeWeaponsRack");
        var massiveWeaponStorage = ThingDef.Named("Storage_MassiveWeaponsRack");

        var tribalStorageSetting = new StorageSettings { Priority = StoragePriority.Important };
        var smallStorageSetting = new StorageSettings { Priority = StoragePriority.Important };
        var largeStorageSetting = new StorageSettings { Priority = StoragePriority.Important };
        var massiveStorageSetting = new StorageSettings { Priority = StoragePriority.Important };

        foreach (var weapon in allWeapons)
        {
            var massToCheck = weapon.BaseMass;
            if (weapon.weaponTags?.Contains("UltratechMelee") == true)
            {
                massToCheck *= 2;
            }

            switch (massToCheck)
            {
                case >= 7f:
                    massiveStorageSetting.filter.SetAllow(weapon, true);
                    break;
                case > 3f:
                    largeStorageSetting.filter.SetAllow(weapon, true);
                    break;
                default:
                    smallStorageSetting.filter.SetAllow(weapon, true);
                    break;
            }

            tribalStorageSetting.filter.SetAllow(weapon, true);
        }

        tribalStorageSetting.filter.SetAllow(ThingCategoryDef.Named("Grenades"), true);

        tribalWeaponStorage.building.fixedStorageSettings = tribalStorageSetting;
        tribalWeaponStorage.building.defaultStorageSettings = tribalStorageSetting;
        smallWeaponStorage.building.fixedStorageSettings = smallStorageSetting;
        smallWeaponStorage.building.defaultStorageSettings = smallStorageSetting;
        largeWeaponStorage.building.fixedStorageSettings = largeStorageSetting;
        largeWeaponStorage.building.defaultStorageSettings = largeStorageSetting;
        massiveWeaponStorage.building.fixedStorageSettings = massiveStorageSetting;
        massiveWeaponStorage.building.defaultStorageSettings = massiveStorageSetting;
    }
}