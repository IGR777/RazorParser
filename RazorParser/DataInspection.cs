using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Cirrious.MvvmCross.ViewModels;

namespace TechMan.Data
{
	public class DataInspection : MvxNotifyPropertyChanged
	{
		public DataInspection ()
		{
			Damage = new ObservableCollection<DataDamageItem> ();
			Options = new List<DataOptionItem> ();
			InspectionImages = new ObservableCollection<DataInspectionImage> ();
			GenericOptions = new List<DataOptionItem> ();
			BodyOptions = new List<HGVBodyClassOption> ();
			Tyres = new ObservableCollection<DataTyreItem> ();
		}

		public List<DataOptionItem> GenericOptions{ get; set; }

		public string InspectionEntityId { get; set; }

		public int InspectionID { get; set; }

		public Guid HandheldGUID { get; set; }

		public int InspCompanyID { get; set; }

		public int InspectorID { get; set; }

		public DateTime InspectionDate { get; set; }

		public DateTime InspectionStartDate { get; set; }

		public InspectionState InspectionState { get; set; }

        public int AutoRepairActionMatrixID { get; set; }
        public int RefurbSetID { get; set; }
        public int PriceMatrixID { get; set; }
        public int PartPriceMatrixID { get; set; }
        public int InspectionType { get; set; }    

		#region vehicle identification

		String _barcode;

		public string Barcode {
			get { return _barcode; }
			set {
				_barcode = value;
				RaisePropertyChanged (() => Barcode);
			}
		}

        public string GroupCode { get; set; }
		public string LotNo { get; set; }

		public string VehicleReference { get; set; }

		string _vin;

		public string VIN {
			get {
				return _vin;
			}
			set {
				_vin = value;
				RaisePropertyChanged (() => VIN);
			}
		}

		string _registration;

		public string Registration {
			get {
				return _registration;
			}
			set {
				_registration = value;
				RaisePropertyChanged (() => Registration);
			}
		}

		CountryState _registrationState;

		public CountryState RegistrationState {
			get { return _registrationState; }
			set {
				_registrationState = value;
				RaisePropertyChanged (() => RegistrationState);
			}
		}

		Month _builMonth;

		public Month BuildMonth {
			get { return _builMonth; }
			set {
				_builMonth = value;
				RaisePropertyChanged (() => BuildMonth);
			}
		}

		int? _buildYear;

		public int? BuildYear {
			get { return _buildYear; }
			set {
				_buildYear = value;
				RaisePropertyChanged (() => BuildYear);
			}
		}

		int? _complianceYear;

		public int? ComplianceYear {
			get { return _complianceYear; }
			set {
				_complianceYear = value;
				RaisePropertyChanged (() => ComplianceYear);
			}
		}

		Month _complianceMonth;

		public Month ComplianceMonth {
			get { return _complianceMonth; }
			set {
				_complianceMonth = value;
				RaisePropertyChanged (() => ComplianceMonth);
			}
		}

		DateTime? _registrationExpiry;

		public DateTime? RegistrationExpiry {
			get { return _registrationExpiry; }
			set {
				_registrationExpiry = value;
				RaisePropertyChanged (() => RegistrationExpiry);
			}
		}

		#endregion

		public String VehicleDescription {
			get {
				return string.Format ("{0} {1}", this.Make.Description, this.Model);
			}
		}

		#region vehicle specific

		Make _make;

		public Make Make {
			get { return _make; }
			set {
				_make = value;
				RaisePropertyChanged (() => Make);
			}
		}

		ModelClass _modelClass;

		public ModelClass ModelClass {
			get { return _modelClass; }
			set {
				_modelClass = value;
				RaisePropertyChanged (() => ModelClass);
			}
		}

		string _model;

		public string Model {
			get {
				return _model;
			}
			set {
				_model = value;
				RaisePropertyChanged (() => Model);
			}
		}

		public string ModelCode { get; set; }

		public string Variant { get; set; }

		public string VariantCode { get; set; }

		DriverSide _driverSide;

		public DriverSide DriverSide {
			get { return _driverSide; }
			set {
				_driverSide = value;
				RaisePropertyChanged (() => DriverSide);
			}
		}

		#endregion

		#region vehicle engine

		GearBox _gearBox;

		public GearBox GearBox {
			get { return _gearBox; }
			set {
				_gearBox = value;
				RaisePropertyChanged (() => GearBox);
			}
		}

		int _gears;

		public int Gears {
			get { return _gears; }
			set {
				_gears = value;
				RaisePropertyChanged (() => Gears);
			}
		}

		FuelLevel _fuelLevel;

		public FuelLevel FuelLevel {
			get { return _fuelLevel; }
			set {
				_fuelLevel = value;
				RaisePropertyChanged (() => FuelLevel);
			}
		}

		OilLevelAction _oilLevelAction;

		public OilLevelAction OilLevelAction {
			get { return _oilLevelAction; }
			set {
				_oilLevelAction = value;
				RaisePropertyChanged (() => OilLevelAction);
			}
		}

		OilLevel _oilLevel;

		public OilLevel OilLevel {
			get { return _oilLevel; }
			set {
				_oilLevel = value;
				RaisePropertyChanged (() => OilLevel);
			}
		}

		WaterLevelAction _waterLevelAction;

		public WaterLevelAction WaterLevelAction {
			get { return _waterLevelAction; }
			set {
				_waterLevelAction = value;
				RaisePropertyChanged (() => WaterLevelAction);
			}
		}

		WaterLevel _waterLevel;

		public WaterLevel WaterLevel {
			get { return _waterLevel; }
			set {
				_waterLevel = value;
				RaisePropertyChanged (() => WaterLevel);
			}
		}

		MissingOdoReason _missingOdoReason;

		public MissingOdoReason MissingOdoReason {
			get { return _missingOdoReason; }
			set {
				_missingOdoReason = value;
				RaisePropertyChanged (() => MissingOdoReason);
			}
		}

		EngineCondition _engineCondition;

		public EngineCondition EngineCondition {
			get { return _engineCondition; }
			set {
				_engineCondition = value;
				RaisePropertyChanged (() => EngineCondition);
			}
		}

		SpeedoChangeReason _speedoChangeReason;

		public SpeedoChangeReason SpeedoChangeReason {
			get { return _speedoChangeReason; }
			set {
				_speedoChangeReason = value;
				RaisePropertyChanged (() => SpeedoChangeReason);
			}
		}
        int? _mileageatChange;
        public int? MileageatChange
        {
            get
            {
                if (_mileageatChange == 0) return null;
                return _mileageatChange;
            }
            set
            {
                _mileageatChange = value;
                RaisePropertyChanged(() => MileageatChange);
            }
        }
		DriveType _driveAndTransmission;

		public DriveType DriveAndTransmission {
			get { return _driveAndTransmission; }
			set {
				_driveAndTransmission = value;
				RaisePropertyChanged (() => DriveAndTransmission);
			}
		}

		FuelType _fuel;

		public FuelType Fuel {
			get { return _fuel; }
			set {
				_fuel = value;
				RaisePropertyChanged (() => Fuel);
			}
		}

		Delivery _delivery;

		public Delivery Delivery {
			get { return _delivery; }
			set {
				_delivery = value;
				RaisePropertyChanged (() => Delivery);
			}
		}

		public string EngineNo { get; set; }

		public decimal EngineSize { get; set; }

		EngineSizeUnit _engineSizeUnit;

		public EngineSizeUnit EngineSizeUnit {
			get { return _engineSizeUnit; }
			set {
				_engineSizeUnit = value;
				RaisePropertyChanged (() => EngineSizeUnit);
			}
		}

	    int _mileage;
		public int Mileage{
        	get{ return _mileage; }
			set {
				_mileage = value;
				RaisePropertyChanged (() => Mileage);
			}
		}

		int _confirmMileage;
		public int ConfirmMileage{
			get{ return _confirmMileage; }
			set {
				_confirmMileage = value;
				RaisePropertyChanged (() => ConfirmMileage);
			}
		}
		string _confirmClaimNumber= String.Empty;
		public string ConfirmClaimNumber{
			get{ return _confirmClaimNumber; }
			set {
				_confirmClaimNumber = value;
				RaisePropertyChanged (() => ConfirmClaimNumber);
			}
		}

		OdometrUnit _odometrUnit;

		public OdometrUnit OdometrUnit {
			get { return _odometrUnit; }
			set {
				_odometrUnit = value;
				RaisePropertyChanged (() => OdometrUnit);
			}
		}

		string odometerImageFile;

		public string OdometerImageFile {
			get {
				return odometerImageFile;
			}
			set {
				odometerImageFile = value;
				RaisePropertyChanged (() => OdometerImageFile);
			}
		}

		#endregion

		#region vehicle body

		BodyPlan _bodyPlan;

		public BodyPlan BodyPlan {
			get { return _bodyPlan; }
			set {
				_bodyPlan = value;
				RaisePropertyChanged (() => BodyPlan);
			}
		}

		public string ChassisNo { get; set; }

		short _doors;

		public short Doors {
			get { return _doors; }
			set {
				_doors = value;
				RaisePropertyChanged (() => Doors);
			}
		}

		short _maxOccupancy;

		public short MaxOccupancy {
			get { return _maxOccupancy; }
			set {
				_maxOccupancy = value;
				RaisePropertyChanged (() => MaxOccupancy);
			}
		}

		short _plates;

		public short Plates {
			get { return _plates; }
			set {
				_plates = value;
				RaisePropertyChanged (() => Plates);
			}
		}

		SpareTyreType _spareTyreType;

		public SpareTyreType SpareTyreType {
			get { return _spareTyreType; }
			set {
				_spareTyreType = value;
				RaisePropertyChanged (() => SpareTyreType);
			}
		}

		public string PaintCode { get; set; }

		public string PaintDescription { get; set; }

		GenColor _colour;

		public GenColor Colour {
			get { return _colour; }
			set {
				_colour = value;
				RaisePropertyChanged (() => Colour);
			}
		}

		#endregion

		public List<DataOptionItem> Options { get; set; }

		public ObservableCollection<DataInspectionImage> InspectionImages { get; set; }

		public bool ServBook { get; set; }

		ServiceHistory _serviceHistory;

		public ServiceHistory ServiceHistory {
			get { return _serviceHistory; }
			set {
				_serviceHistory = value;
				RaisePropertyChanged (() => ServiceHistory);
			}
		}

		DateTime? _lastServiceDate;

		public DateTime? LastServiceDate {
			get { return _lastServiceDate; }
			set {
				_lastServiceDate = value;
				RaisePropertyChanged (() => LastServiceDate);
			}
		}

		public RepairActionRule AutoRepairActionRule{ get; set; }

        public int? _lastServiceOdometer { get; set; }
        public int? LastServiceOdometer
        {
            get {
                if (_lastServiceOdometer==0) return null;
                return _lastServiceOdometer; }
            set
            {
                _lastServiceOdometer = value;
                RaisePropertyChanged(() => LastServiceOdometer);
            }
        }

		string _lastServiceBy;

		public string LastServiceBy {
			get { return _lastServiceBy; }
			set {
				_lastServiceBy = value;
				RaisePropertyChanged (() => LastServiceBy);
			}
		}

		public bool RequiresService { get; set; }

		public string ServiceHistoryDescription { get; set; }

		public bool HandBook { get; set; }

		public bool MasterKey { get; set; }

		public bool SpareKey { get; set; }

		public bool SpareRemote { get; set; }

		public bool Remote { get; set; }

		public bool Etag { get; set; }

		public bool FuelCard { get; set; }

		public bool LockingWheelNut { get; set; }

		public string InspectionNotes { get; set; }

		InspectionResult _result;

		public InspectionResult Result {
			get { return _result; }
			set {
				_result = value;
				RaisePropertyChanged (() => Result);
			}
		}

		InspectionCondition _inspectionCondition;

		public InspectionCondition InspectionCondition {
			get { return _inspectionCondition; }
			set {
				_inspectionCondition = value;
				RaisePropertyChanged (() => InspectionCondition);
			}
		}

		VehicleCondition _vehicleCondition;

		public VehicleCondition VehicleCondition {
			get { return _vehicleCondition; }
			set {
				_vehicleCondition = value;
				RaisePropertyChanged (() => VehicleCondition);
			}
		}

		public ObservableCollection<DataDamageItem> Damage { get; set; }

		public DataTyreItem LeftFront { get; set; }

		public DataTyreItem RightFront { get; set; }

		public DataTyreItem RightRear { get; set; }

		public DataTyreItem LeftRear { get; set; }

		public DataTyreItem Spare { get; set; }

		public string InspSigFile { get; set; }

		public string DrivSigFile { get; set; }

		public DateTime DateMarkedAsCompleted { get; set; }

		public DateTime DateSentFromMobile { get; set; }

		public int InspVehicleID { get; set; }

		public int InspGrade { get; set; }

		InspectionGrade _grade;

		public InspectionGrade Grade {
			get { return _grade; }
			set {
				_grade = value;
				if (_grade != null) {
					InspGrade = _grade.ID;
				}
				RaisePropertyChanged (() => Grade);
			}
		}

		public string DrivSigName { get; set; }

		public bool CustomerPresent { get; set; }

		public bool AgreeToTandC { get; set; }

		public String ContractName {
			get {
				return _selectedDataContract != null ? _selectedDataContract.ContractName : "";
			}
		}

		#region Location fields

		public Location StorageLocation { get; set; }

		string _customerName;

		public string CustomerName {
			get { return _customerName; }
			set {
				_customerName = value;
				RaisePropertyChanged (() => CustomerName);
			}
		}

		string _companyName;

		public string CompanyName {
			get { return _companyName; }
			set {
				_companyName = value;
				RaisePropertyChanged (() => CompanyName);
			}
		}

		string _address1;

		public string Address1 {
			get { return _address1; }
			set {
				_address1 = value;
				RaisePropertyChanged (() => Address1);
			}
		}

		string _postCode;

		public string PostCode {
			get { return _postCode; }
			set {
				_postCode = value;
				RaisePropertyChanged (() => PostCode);
			}
		}

		CountryState _addressState;

		public CountryState AddressState {
			get { return _addressState; }
			set {
				_addressState = value;
				RaisePropertyChanged (() => AddressState);
			}
		}

		string _telephone1;

		public string Telephone1 {
			get { return _telephone1; }
			set {
				_telephone1 = value;
				RaisePropertyChanged (() => Telephone1);
			}
		}

		string _telephone2;

		public string Telephone2 {
			get { return _telephone2; }
			set {
				_telephone2 = value;
				RaisePropertyChanged (() => Telephone2);
			}
		}

		string _emailAddresses;

		public string EmailAddresses {
			get {
				return _emailAddresses;
			}
			set {
				_emailAddresses = value;
				RaisePropertyChanged (() => EmailAddresses);
			}
		}

		LocationType _locationtype;

		public LocationType LocationType {
			get { return _locationtype; }
			set {
				_locationtype = value;
				RaisePropertyChanged (() => LocationType);
			}
		}

		#endregion

		Seller _seller;

		public Seller Seller {
			get {
				return _seller;
			}
			set {
				_seller = value;
				RaisePropertyChanged (() => Seller);
			}
		}

		BookInClass _bookinClass;

		public BookInClass BookInClass {
			get {
				return _bookinClass;
			}
			set {
				_bookinClass = value;
				RaisePropertyChanged (() => BookInClass);
			}
		}

		public string Client { get; set; }

		public string SellerRef { get; set; }

		public string SellerCategory { get; set; }

		public string Category { get; set; }

		DateTime? _bookInDate;

		public DateTime? BookingDate {
			get { return _bookInDate; }
			set {
				_bookInDate = value;
				RaisePropertyChanged (() => BookingDate);
			}
		}

		Plant _plant;

		public Plant Plant {
			get {
				return _plant;
			}
			set {
				_plant = value;
				RaisePropertyChanged (() => Plant);
			}
		}

		string _serviceBookImageFile;

		public string ServiceBookImageFile {
			get {
				return _serviceBookImageFile;
			}
			set {
				_serviceBookImageFile = value;
				RaisePropertyChanged (() => ServiceBookImageFile);
			}
		}

		string _wheelImageFile;

		public string WheelImageFile {
			get {
				return _wheelImageFile;
			}
			set {
				_wheelImageFile = value;
				RaisePropertyChanged (() => WheelImageFile);
			}
		}

		string _claimNumber = String.Empty;

		public string ClaimNumber {
			get {
				return _claimNumber;
			}
			set {
				_claimNumber = value;
				RaisePropertyChanged (() => ClaimNumber);
			}
		}

		PaintType _paintType;

		public PaintType PaintType {
			get { return _paintType; }
			set {
				_paintType = value;
				RaisePropertyChanged (() => PaintType);
			}
		}

		InteriorTrim _interiorTrim;

		public InteriorTrim InteriorTrim {
			get { return _interiorTrim; }
			set {
				_interiorTrim = value;
				RaisePropertyChanged (() => InteriorTrim);
			}
		}

		GenColor _interiorcolour;

		public GenColor InteriorColour {
			get { return _interiorcolour; }
			set {
				_interiorcolour = value;
				RaisePropertyChanged (() => InteriorColour);
			}
		}

		public bool RoadTax { get; set; }

		public bool V5 { get; set; }

		DateTime? _motExpiryDate;

		public DateTime? MOTExpiryDate {
			get { return _motExpiryDate; }
			set {
				_motExpiryDate = value;
				RaisePropertyChanged (() => MOTExpiryDate);
			}
		}

		DateTime? _taxExpiryDate;

		public DateTime? TaxExpiryDate {
			get { return _taxExpiryDate; }
			set {
				_taxExpiryDate = value;
				RaisePropertyChanged (() => TaxExpiryDate);
			}
		}

		public int FormerOwners { get; set; }

		public int ServiceStamps { get; set; }

		public int InspContractID { get; set; }

		DataContract _selectedDataContract;

		public DataContract SelectedDataContract {
			get {
				return _selectedDataContract;
			}
			set {
				_selectedDataContract = value;
				if (_selectedDataContract != null) {
					InspContractID = _selectedDataContract.ContractID;
					CustomerAdminFee = _selectedDataContract.CustomerAdminFee;
					// If fields have values they shouldn't overwrite
					if (EmailAddresses == null) {
						EmailAddresses = _selectedDataContract.ContractEmailAddress;
					}
					if (Address1 == null) {
						Address1 = _selectedDataContract.ContractAddress;
					}
					if (Telephone1 == null) {
						Telephone1 = _selectedDataContract.ContractTelephone1;
					}
				}
				RaisePropertyChanged (() => SelectedDataContract);
			}
		}

		decimal _excessMileageCharge;

		public decimal ExcessMileageCharge {
			get {
				return _excessMileageCharge;
			}
			set {
				_excessMileageCharge = value;
				RaisePropertyChanged (() => ExcessMileageCharge);
			}
		}

		decimal _missedServCost;

		public decimal MissedServCost {
			get {
				return _missedServCost;
			}
			set {
				_missedServCost = value;
				RaisePropertyChanged (() => MissedServCost);
			}
		}

		public decimal CustomerAdminFee { get; set; }

		public string AutoGrade { get; set; }

		public DateTime? DateUploaded { get; set; }

		public ObservableCollection<DataTyreItem> Tyres { get; set; }

		public string TyreSize { get; set; }

		public int SpareKeys { get; set; }

		#region HGV fields

		public HGVCabClass HGVCabClass { get; set; }

		public HGVSuspension HGVSuspension { get; set; }

		public int Wheelbase { get; set; }

		public int BodyLength { get; set; }

		public HGVFlatCar AxleConfig { get; set; }

		public HGVBodyClass BodyClass { get; set; }

		public HGVBodyCrane HGVBodyCrane { get; set; }

		public string HGVBodyCraneMake { get; set; }

		public string HGVBodyCraneModel { get; set; }

		public string HGVBodyCraneSitting { get; set; }

		public string HGVBodyCraneAge { get; set; }

		public List<HGVBodyClassOption> BodyOptions { get; set; }

		#endregion

		MaterialGroup _materialGroup;

		public MaterialGroup MaterialGroup {
			get {
				return _materialGroup;
			}
			set {
				_materialGroup = value;
				RaisePropertyChanged (() => MaterialGroup);
			}
		}

		StorageLocation _storageLocations;

		public StorageLocation StorageLocations {
			get {
				return _storageLocations;
			}
			set {
				_storageLocations = value;
				RaisePropertyChanged (() => StorageLocations);
			}
		}

		SellingCategory _sellingCategory;

		public SellingCategory SellingCategory {
			get {
				return _sellingCategory;
			}
			set {
				_sellingCategory = value;
				RaisePropertyChanged (() => SellingCategory);
			}
		}

		string _address3;

		public string Address3 {
			get { return _address3; }
			set {
				_address3 = value;
				RaisePropertyChanged (() => Address3);
			}
		}

		public string CustomerNotes { get; set; }

		#region Abort inspection

		AbortInspectionReason _abortInspectionReason;

		public AbortInspectionReason AbortInspectionReason {
			get {
				return _abortInspectionReason;
			}
			set {
				_abortInspectionReason = value;
				RaisePropertyChanged (() => AbortInspectionReason);
			}
		}

		string _abortInspectionImageFile;

		public string AbortInspectionImageFile {
			get {
				return _abortInspectionImageFile;
			}
			set {
				_abortInspectionImageFile = value;
				RaisePropertyChanged (() => AbortInspectionImageFile);
			}
		}

		public string AbortInspectionDate { get; set; }

		public string AbortInspectionNotes { get; set; }

		#endregion

		ServiceBookType _serviceBookType;

		public ServiceBookType ServiceBookType {
			get {
				return _serviceBookType;
			}
			set {
				_serviceBookType = value;
				RaisePropertyChanged (() => ServiceBookType);
			}
		}

		public int RuleSetID { get; set; }
       public int Age
        {
            get
            {
                if (RegistrationExpiry != null && RegistrationExpiry!= DateTime.MinValue)
                    return ((DateTime.Now.Year - Convert.ToDateTime(RegistrationExpiry).Year) * 12) + DateTime.Now.Month - Convert.ToDateTime(RegistrationExpiry).Month;
                else if(BuildYear!=null)
                    return (DateTime.Now.Year - Convert.ToInt32(BuildYear)) * 12;
                else 
                    return 0;
            }
       }
        
        bool _shouldHideAssured;
        public bool ShouldHideAssured
        {
            get
            {
                return _shouldHideAssured;
            }
            set
            {
                _shouldHideAssured = value;
                RaisePropertyChanged(() => ShouldHideAssured);
            }
        }


		GenColor _trimColour;

		public GenColor TrimColour {
			get {
				return _trimColour;
			}
			set {
				_trimColour = value;
				RaisePropertyChanged (() => TrimColour);
			}
		}

		GenColor _wrapColour;

		public GenColor WrapColour {
			get {
				return _wrapColour;
			}
			set {
				_wrapColour = value;
				RaisePropertyChanged (() => WrapColour);
			}
		}

		string _cO2;

		public string CO2 {
			get { return _cO2; }
			set {
				_cO2 = value;
				RaisePropertyChanged (() => CO2);
			}
		}

		public bool ChargeCablePresent { get; set; }

		BatteryChargeLevel _batteryChargeLevel;

		public BatteryChargeLevel BatteryChargeLevel {
			get {
				return _batteryChargeLevel;
			}
			set {
				_batteryChargeLevel = value;
				RaisePropertyChanged (() => BatteryChargeLevel);
			}
		}

		AdBlueLevel _adBlueLevel;

		public AdBlueLevel AdBlueLevel {
			get {
				return _adBlueLevel;
			}
			set {
				_adBlueLevel = value;
				RaisePropertyChanged (() => AdBlueLevel);
			}
		}

		V5Reason _v5Reason;
        public V5Reason V5Reason
        {
            get
            {
                return _v5Reason;
            }
            set
            {
                _v5Reason = value;
                RaisePropertyChanged(() => V5Reason);
            }
		}
        Odour _odour;
                public Odour Odour
        {
            get
            {
                return _odour;
            }
            set
            {
                _odour = value;
                RaisePropertyChanged(() => Odour);
            }
		}		AssuredRejectReason _assuredRejectReason;
        public AssuredRejectReason AssuredRejectReason
        {
            get
            {
                return _assuredRejectReason;
            }
            set
            {
                _assuredRejectReason = value;
                RaisePropertyChanged(() => AssuredRejectReason);
            }
		}
        DisagreeReason _disagreeReason;
        public DisagreeReason DisagreeReason
        {
            get
            {
                return _disagreeReason;
            }
            set
            {
                _disagreeReason = value;
                RaisePropertyChanged(() => DisagreeReason);
            }
        }
        public int VehicleClassID { get; set; }
        public string VehicleClassDescription { get; set; }


        Disclaimer _disclaimer;
        public Disclaimer Disclaimer
        {
            get
            {
                return _disclaimer;
            }
            set
            {
                _disclaimer = value;
                RaisePropertyChanged(() => Disclaimer);
            }
        }

        DamageWaiver _damageWaiver;
        public DamageWaiver DamageWaiver
        {
            get
            {
                return _damageWaiver;
            }
            set
            {
                _damageWaiver = value;
                RaisePropertyChanged(() => DamageWaiver);
            }
        }

		[XmlIgnoreAttribute]
		public byte[] CustomerSignature{ get; set; }

		[XmlIgnoreAttribute]
		public byte[] InspectorSignature{ get; set; }

		public string SerializedCustomerSignature{ get; set; }

		public string SerializedInspectorSignature{ get; set; }

        #region Assured Inspection

		public int AssuredListID { get; set; }

		public List<AssuredDataQuestionAnswerItem> AssuredQuestionsAnswers { get; set; }

		#endregion

		#region Survey Inspection

		public int SurveyListID { get; set; }

		public string QuestionnaireComments { get; set; }
        public string AutoRepairActionMatrixName { get; set; }
        public string PriceMatrixName { get; set; }
        public string SchemaName { get; set; }

        public List<SurveyDataQuestionAnswerItem> SurveyQuestionsAnswers { get; set; }

        public List<ServiceItem> ServiceDetails { get; set; }

		#endregion

		#region Signature

		public bool Disagree { get; set; }

		public string DisagreeReasonText { get; set; }

		#endregion

		#region Droid search result multi

		public string ModelCellText {
			get {
				return String.Format ("{0}, {1}", this.Model, this.Variant);
			}
		}

		public string EngineCellText {
			get {
				return String.Format ("{0} {1}, {2}, {3}",
					this.EngineSize.ToString ().TrimEnd ('0').TrimEnd ('.'),
					this.EngineSizeUnit,
					this.Fuel.Description,
					this.BodyPlan.Description);
			}
		}

		public string TransmissionCellText {
			get {
				return String.Format ("{0} {1}, {2}",
					this.GearBox.Description,
					this.GearBox.Code.Trim ('0'),
					this.DriveAndTransmission.Description);
			}
		}

		#endregion

		#region LCV

		public LCVStyle LCVStyle { get; set; }

		public LCVMainStyle LCVMainStyle { get; set; }

		public LCVLength LCVLength { get; set; }

		public LCVMaterial LCVMaterial { get; set; }

		public LCVHeight LCVHeight { get; set; }

		public LCVType LCVType { get; set; }

		public GenColor LCVBodyColour1 { get; set; }

		public GenColor LCVBodyColour2 { get; set; }

		public LCVDecalType LCVDecalType { get; set; }

		public LCVDecalSize LCVDecalSize { get; set; }

		public LCVWrap LCVWrap { get; set; }

		#endregion

		#region Background sending

		[XmlIgnoreAttribute]
		public int QueueNumber { get; set; }

		[XmlIgnoreAttribute]
		public int AttemptsNumber { get; set; }

		public string BackgroundSendError { get; set; }

		//Completed inspection is editing
		public bool IsEditing { get; set; }

		#endregion
	}
}