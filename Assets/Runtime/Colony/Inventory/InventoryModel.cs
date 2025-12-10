using System;
using System.Collections.Generic;
using Runtime.Colony.GameResources;
using Runtime.Colony.ModelCollections;

namespace Runtime.Colony.Inventory
{
    //TODO: Переписать на реактивную коллекцию
    public class InventoryModel : UniformModelCollection<CellModel>
    {
        public List<CellModel> Cells { get; } = new();

        public InventoryModel(int size) : base(null)
        {
            CreateEmptyCells(size);
        }

        public bool TryAddItem(IInventoryItem item, int amount)
        {
            if (!CanFit(item, amount, out var targets))
            {
                return false;
            }

            var remaining = amount;

            foreach (var (cell, free) in targets)
            {
                var toAdd = Math.Min(free, remaining);
                cell.TryAdd(item, toAdd);
                remaining -= toAdd;

                if (remaining == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanFit(IInventoryItem item, int amount, out List<(CellModel cell, int free)> targets)
        {
            targets = new List<(CellModel, int)>();
            var remaining = amount;

            foreach (var cell in Cells)
            {
                if (cell.Item != null && IsSameItem(cell.Item, item))
                {
                    var free = item.MaxAmount - cell.Amount;

                    if (free > 0)
                    {
                        targets.Add((cell, free));
                        remaining -= Math.Min(free, remaining);

                        if (remaining <= 0)
                        {
                            return true;
                        }
                    }
                }
            }

            foreach (var cell in Cells)
            {
                if (cell.Item == null)
                {
                    var free = item.MaxAmount;
                    targets.Add((cell, free));
                    remaining -= Math.Min(free, remaining);

                    if (remaining <= 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryRemoveItem(IInventoryItem item, int amount)
        {
            var remaining = amount;

            for (var i = Cells.Count - 1; i >= 0; i--)
            {
                var cell = Cells[i];

                if (cell.Item != null && IsSameItem(cell.Item, item))
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

        private void CreateEmptyCells(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var cell = new CellModel();

                Cells.Add(cell);
            }
        }

        private bool IsSameItem(IInventoryItem a, IInventoryItem b)
        {
            if (a is ResourceModel resourceA && b is ResourceModel resourceB)
            {
                return resourceA.Description == resourceB.Description;
            }

            return false;
        }

        protected override CellModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        protected override CellModel CreateModel()
        {
            throw new NotImplementedException();
        }
    }
}