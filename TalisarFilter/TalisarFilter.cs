#region BSD 3-Clause License

/*
 * TalisarFilter
 * 
 * Copyright (c) 2017, Arne Peirs
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors
 *    may be used to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#endregion

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using KSP.Localization;
using KSP.UI.Screens;
using RUI.Icons.Selectable;
using UnityEngine;

namespace TalisarFilter
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class TalisarFilter : MonoBehaviour
    {
        private readonly List<string> manufacturers = new List<string>();
        private readonly List<AvailablePart> parts = new List<AvailablePart>();
        private readonly string category = "#autoLOC_453547";
        private readonly string categoryTitle = "Talisar";

        public void Awake()
        {
            AddManufacturers();

            parts.Clear();
            int count = PartLoader.LoadedPartsList.Count;
            for (int i = 0; i < count; ++i)
            {
                AvailablePart avPart = PartLoader.LoadedPartsList[i];
                if (!avPart.partPrefab)
                    continue;

                if (manufacturers.Contains(avPart.manufacturer))
                    parts.Add(avPart);
            }

            RemovePartCategory();

            Debug.Log($"[TF] - Added {parts.Count} parts to filter");
            if (parts.Count > 0)
                GameEvents.onGUIEditorToolbarReady.Add(SubCategories);
        }

        private void AddManufacturers()
        {
            manufacturers.Add(Localizer.Format("#LOC_Tal_Cargo_Manufacturer"));
            manufacturers.Add(Localizer.Format("#LOC_Tal_Spherical_Manufacturer"));
            manufacturers.Add(Localizer.Format("#LOC_Tal_Toroidal_Manufacturer"));
            manufacturers.Add(Localizer.Format("#LOC_Tal_Science_Manufacturer"));
            manufacturers.Add(Localizer.Format("#LOC_Tal_Structural_Manufacturer"));
        }

        private void RemovePartCategory()
        {
            int count = parts.Count;
            for (int i = 0; i < count; ++i)
            {
                AvailablePart avPart = parts[i];
                avPart.category = PartCategories.none;
            }
        }

        private bool EditorItemsFilter(AvailablePart avPart)
        {
            return parts.Contains(avPart);
        }

        private void SubCategories()
        {
            Icon icon = GenerateIcon(categoryTitle);
            PartCategorizer.Category filter = PartCategorizer.Instance.filters.Find(f => f.button.categoryName == category);
            PartCategorizer.AddCustomSubcategoryFilter(filter, categoryTitle, categoryTitle, icon, EditorItemsFilter);
        }

        private Icon GenerateIcon(string iconName)
        {
            Texture2D normalIcon = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            string normalIconFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), iconName + "_N.png");
            normalIcon.LoadImage(File.ReadAllBytes(normalIconFile));

            Texture2D selectedIcon = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            string selectedIconFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), iconName + "_S.png");
            selectedIcon.LoadImage(File.ReadAllBytes(selectedIconFile));

            Debug.Log($"[TF] - Adding icon for {categoryTitle}");
            Icon icon = new Icon(iconName + "Icon", normalIcon, selectedIcon);
            return icon;
        }
    }
}
