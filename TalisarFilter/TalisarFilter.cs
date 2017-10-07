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
        private readonly string normalIcon = "Talisar_N.png";
        private readonly string selectedIcon = "Talisar_S.png";

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
            manufacturers.Add(Localizer.Format("#Talloc_Cargo_Manufacturer"));
            manufacturers.Add(Localizer.Format("#Talloc_Spherical_Manufacturer"));
            manufacturers.Add(Localizer.Format("#Talloc_Toroidal_Manufacturer"));
            manufacturers.Add(Localizer.Format("#Talloc_Science_Manufacturer"));
            manufacturers.Add(Localizer.Format("#Talloc_Structural_Manufacturer"));
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
            PartCategorizer.Category filter = PartCategorizer.Instance.filters.Find(f => f.button.categorydisplayName == category);
            if (filter == null)
            {
                // This can only happen if there are major changes in the KSP code
                Debug.Log($"[TF] Cannot find the 'Filter by Function' button for category: {categoryTitle}");
                return;
            }
            PartCategorizer.AddCustomSubcategoryFilter(filter, categoryTitle, categoryTitle, GenerateIcon(), EditorItemsFilter);
        }

        private Icon GenerateIcon()
        {
            Texture2D normalTexture = GameDatabase.Instance.GetTexture(normalIcon, false);
            Texture2D selectedTexture = GameDatabase.Instance.GetTexture(selectedIcon, false);

            Debug.Log($"[TF] Adding icon for {categoryTitle}");
            return new Icon(categoryTitle + "-icon", normalTexture, selectedTexture);
        }
    }
}
