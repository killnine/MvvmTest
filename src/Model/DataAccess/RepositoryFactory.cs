namespace Model.DataAccess
{
    public static class RepositoryFactory
    {
        private static IEquipmentRepository _equipmentRepository;
        public static IEquipmentRepository EquipmentRepository
        {
            get { return _equipmentRepository ?? new EquipmentRepository(); }
            set { _equipmentRepository = value; }
        }
    }
}
