using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.Inventory.Cell;
using Runtime.ModelCollections;
using Runtime.Utilities;

namespace Runtime.Colony.Inventory
{
    public class InventoryModel : UniformModelCollection<CellModel>
    {
        public event Action<CellModel> OnItemChanged;

        public int Size;

        public InventoryModel(int size) : base(null)
        {
            Size = size;

            //TODO: Где это должно быть?
            for (var i = 0; i < size; i++)
            {
                Create();
            }
        }

        protected override CellModel CreateModel()
        {
            var cell = new CellModel();

            return cell;
        }

        //TODO: Добавить получение IInventoryItem
        protected override CellModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var cell = new CellModel();

            var amount = data.GetInt("amount");

            cell.TryAdd(null, amount);

            return cell;
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

                OnItemChanged?.Invoke(cell);

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

            foreach (var pair in Models)
            {
                if (pair.Value.Item != null && IsSameItem(pair.Value.Item, item))
                {
                    var free = item.MaxAmount - pair.Value.Amount;

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
                if (pair.Value.Item == null)
                {
                    var free = item.MaxAmount;
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

        public bool TryRemoveItem(IInventoryItem item, int amount)
        {
            var remaining = amount;

            for (var i = Models.Count - 1; i >= 0; i--)
            {
                var cell = Models.ElementAt(i).Value;

                if (cell.Item != null && IsSameItem(cell.Item, item))
                {
                    var toRemove = Math.Min(cell.Amount, remaining);

                    if (toRemove > 0)
                    {
                        cell.TryReduce(toRemove);

                        OnItemChanged?.Invoke(cell);

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

        private bool IsSameItem(IInventoryItem itemA, IInventoryItem itemB)
        {
            return itemA.Type == itemB.Type;
        }
    }
}