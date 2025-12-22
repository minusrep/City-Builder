using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.Inventory.Cell;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Inventory
{
    public class InventoryModel : UniformModelCollection<CellModel>
    {
        public int Size;

        private readonly int _maxStackSize;
        private readonly ResourceDescriptionCollection _resourceDescriptions;

        public InventoryModel(int size, int maxStackSize, ResourceDescriptionCollection resourceDescriptions) : base(null)
        {
            Size = size;
            _maxStackSize = maxStackSize;
            _resourceDescriptions = resourceDescriptions;
        }

        public bool TryAddItem(ResourceDescription item, int amount)
        {
            if (!CanFit(item, amount, out var targets))
            {
                return false;
            }

            var remaining = amount;

            foreach (var (cell, free) in targets)
            {
                var toAdd = Math.Min(free, remaining);
                cell.TryAdd(item, toAdd, _maxStackSize);

                remaining -= toAdd;

                if (remaining == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveItem(ResourceDescription item, int amount)
        {
            var remaining = amount;

            for (var i = Models.Count - 1; i >= 0; i--)
            {
                var cell = Models.ElementAt(i).Value;

                if (cell.Resource != null && IsSameItem(cell.Resource, item))
                {
                    var toRemove = Math.Min(cell.Amount, remaining);

                    if (toRemove > 0)
                    {
                        cell.TryReduce(toRemove);

                        remaining -= toRemove;

                        if (remaining == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CanFit(ResourceDescription item, int amount, out List<(CellModel cell, int free)> targets)
        {
            targets = new List<(CellModel, int)>();
            var remaining = amount;

            foreach (var pair in Models)
            {
                if (pair.Value.Resource != null && IsSameItem(pair.Value.Resource, item))
                {
                    var free = _maxStackSize - pair.Value.Amount;

                    if (free > 0)
                    {
                        targets.Add((pair.Value, free));
                        remaining -= Math.Min(free, remaining);

                        if (remaining <= 0)
                        {
                            return true;
                        }
                    }
                }
            }

            foreach (var pair in Models)
            {
                if (pair.Value.Resource == null)
                {
                    var free = _maxStackSize;
                    targets.Add((pair.Value, free));

                    remaining -= Math.Min(free, remaining);

                    if (remaining <= 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override CellModel CreateModel()
        {
            var cell = new CellModel();

            return cell;
        }

        protected override CellModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var cell = new CellModel();
            
            var amount = data.GetInt("amount");
            var resourceId = data.GetString("resource");
            
            cell.TryAdd(_resourceDescriptions.Descriptions[resourceId], amount, _maxStackSize);

            return cell;
        }

        private bool IsSameItem(ResourceDescription itemA, ResourceDescription itemB)
        {
            return itemA.Id == itemB.Id;
        }
    }
}