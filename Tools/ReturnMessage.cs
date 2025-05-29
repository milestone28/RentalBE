
namespace Tools
{
    public static class ReturnMessage
    {
        // 000
        public static readonly string Login = "Successfully login.";
        public static readonly string Refresh = "Successfully refresh.";
        public static readonly string Logout = "Successfully logout.";
        public static readonly string Fetched = "Successfully fetched.";
        public static readonly string Saved = "Successfully saved.";
        public static readonly string Updated = "Successfully updated.";
        public static readonly string Removed = "Successfully removed.";
        public static readonly string ValidTokenPassword = "Password token is valid.";
        public static readonly string PendingRegistration = "Your registration has successfully submitted, please wait for a confirmation on your email.";
        public static readonly string Requested = "Successfully requested.";
        public static readonly string Created = "Successfully created.";

        // 001
        public static readonly string Authorization = "Missing/Invalid token.";
        public static readonly string MissingAuthHeader = "Missing authorization header.";
        public static readonly string InvalidAuthHeader = "Invalid authorization header.";

        // 001 Denied
        public static string AccessRights = "Insufficient access rights.";
        public static string DeniedIPLock = "IP Lock Activated, Access denied at the current IP.";
        public static string DeniedDayLock = "Day Lock Activated, Access denied at the moment.";
        public static string DeniedTimeLock = "Time Lock Activated, Access denied at the moment.";
        public static string DeniedMacAddress = "MAC Address is Activated, Access denied at the current mac address.";

        // 001 Others
        public static readonly string UserDeactivated = "User deactivated please contact administrator.";
        public static readonly string ForgetPasswordError = "If your email exists in our database, you will receive a password recovery link at your email address in a few minutes..";
        public static readonly string ExpiredTokenPassword = "Your password token has expired.";
        public static readonly string NotMatchConfirmPass = "Password and confirm password do not match.";
        public static readonly string WeakPassword = "Can't use password, too weak.";

        // 001 Invalid
        public static string InvalidRequestData = "Invalid request data.";
        public static string InvalidUserId = "Invalid userID.";
        public static string InvalidRightsCode = "Invalid rights code.";
        public static string InvalidDistributorCode = "Invalid distributor code.";
        public static string InvalidGuarantorCode = "Invalid guarantor code.";
        public static string InvalidBranchCode = "Invalid branch.";
        public static string InvalidAccess = "Invalid access, unauthorized.";
        public static string InvalidUserIp = "Invalid user ip.";
        public static string InvalidOldPassword = "Old password is invalid.";
        public static string InvalidCurrency = "Currency is invalid.";
        public static string InvalidCompany = "Invalid company.";
        public static string InvalidOtherCharge = "Invalid other charge.";
        public static string InvalidGCOtherCharge = "Invalid GC other charge.";
        public static string InvalidServiceCharge = "Invalid service charge.";
        public static string InvalidAccountType = "Invalid account type.";
        public static string InvalidStatus = "Invalid status.";
        public static string InvalidCivilStatus = "Invalid civil status.";
        public static string InvalidEmploymentStatus = "Invalid employment status.";
        public static string InvalidScheduleType = "Invalid schedule type.";
        public static string InvalidBonusScheduleType = "Invalid bonus schedule type.";
        public static string InvalidTypeGroupandTypeCode = "Invalid type group & type code.";
        public static string InvalidScheduleClass = "Invalid schedule class.";
        public static string InvalidDividendClass = "Invalid dividend class.";
        public static string InvalidSalesQuotaClass = "Invalid sales quota class.";
        public static string InvalidBonusSales = "Invalid bonus sales requirement.";
        public static string InvalidDenomType = "Invalid denom type.";
        public static string InvalidInventoryNo = "Invalid inventory number.";
        public static string InvalidProductCode = "Invalid product code.";
        public static string RequestPending = "Request is pending.";
        public static string AlreadyApproved = "Request already approved.";
        public static string AlreadyDeclined = "Request already declined.";
        public static string InvalidBatchNo = "Invalid batch number.";
        public static string POCardAlreadyReviewed = "PO Card already reviewed.";
        public static string InvalidCardExpiry = "Invalid card expiry.";
        public static string InvalidDenomORBatchNo = "Invalid denom type or batch no.";
        public static string InvalidStartSerialNo = "Start serial no. is invalid.";
        public static string InvalidEndSerialNo = "End serial no. is invalid.";
        public static string InvalidActivationNo = "Invalid activation number.";
        public static string InvalidPaymentOpt = "Invalid payment option.";
        public static string InvalidTenderedAmt = "Invalid tendered amount.";
        public static string InvalidAmtPaid = "Invalid amount to paid.";
        public static string InvalidChangeAmt = "Invalid change amount.";
        public static string InvalidPin = "Invalid card number or serial number.";
        public static string InvalidApp = "Invalid app.";
        public static string InvalidDev = "Invalid developer.";
        public static string InvalidDeductedCashBond = "Can't be greaterthan or equal to balance.";
        public static string InvalidRequestNo = "Invalid request number.";
        public static string InvalidTodo = "Invalid to do.";
        public static string InvalidProcessType = "Invalid process type.";
        public static string InvalidBillingTrxnNo = "Invalid billing trxn number.";
        public static string InvalidTermID = "Invalid term ID.";
        public static string InvalidUserIdSpecialCharacter = "Invalid UserID should not contain special characters.";

        // 001 Exist
        public static string UserIdExist = "UserID already exist.";
        public static string EmailExist = "Email already exist.";
        public static string UserAlreadySubscribe = "User has already subscribe.";
        public static string RightsCodeExist = "Rights code already exist.";
        public static string UserIpExist = "User IP already exist.";
        public static string UserTimeLockExist = "User timelock already exist.";
        public static string UserDistributorExist = "Distributor already exist.";
        public static string UserDistributorIDExist = "Distributor ID already exist.";
        public static string PasswordExist = "Blocked password already exist.";
        public static string CurrencyExist = "Currency already exist.";
        public static string CompanyExist = "Company already exist.";
        public static string OtherChargeExist = "Other charge exist.";
        public static string ServiceChargeExist = "Service charge exist.";
        public static string BranchCodeExist = "Branch exist.";
        public static string TypeGroupandTypeCodeExist = "Type group & type code already exist.";
        public static string ScheduleTypeExist = "Schedule type already exist.";
        public static string BonusScheduleTypeExist = "Bonus schedule type already exist.";
        public static string DenomExist = "Denom already exist.";
        public static string PaymentOptExist = "Payment option already exist.";
        public static string AppExist = "App already exist.";
        public static string DevExist = "Developer already exist.";
        public static string DevAppExist = "Developer app already exist.";
        public static string DevIPpExist = "Developer IP already exist.";
        public static string ReturnCashbondReqExist = "Error, An existing returned cashbond request has already been made.";
        public static string TermExist = "Term already exist.";
        public static string BranchUserProfileExist = "Branch user profile already exist.";
        public static string DistributorUserProfileExist = "Distributor user profile already exist.";

        // 001 Inactive
        public static string InactiveScheduleType = "Inactive schedule type.";
        public static string InactiveBonusScheduleType = "Inactive bonus schedule type.";
        public static string InactiveDistributorCode = "Inactive distributor code.";
        public static string InactiveGuarantorCodex = "Inactive guarantor code.";
        public static string InactiveBranchCode = "Inactive branch.";
        public static string InactiveProductCode = "Inactive product.";
        public static string InactivePaymentOpt = "Inactive payment option.";
        public static string InactiveOtherCharge = "Inactive other charge.";
        public static string InactiveServiceCharge = "Inactive service charge.";
        public static string InactiveDev = "Inactive developer.";
        public static string InactiveApp = "Inactive app.";
        public static string DeactivatedGuarantor = "Guarantor is deactivated.";
        public static string DeactivatedBonusGuarantor = "Bonus guarantor is deactivated.";
        public static string InactiveTermID = "Inactive term ID.";

        // 001 Required
        public static string RequiredDistributorCode = "Distributor code is required.";
        public static string RequiredBranchCode = "Branch code is required.";
        public static string RequiredGuarantorCode = "Guarantor code is required.";
        public static string RequiredUserRole = "Please select atleast one(1) user role.";
        public static string RequiredDecreptionKey = "Decryption key is required.";
        public static string RequiredPasswordZip = "Password zip is required.";
        public static string RequiredBatchNo = "Batch NO. is required.";
        public static string RequiredDenomType = "Denom type is required.";
        public static string RequiredStartSerialNo = "Start serial no. is required.";
        public static string RequiredEndSerialNo = "End serial no. is required.";
        public static string RequiredUpdateType = "Update type is required.";
        public static string RequiredTodo = "To do is required.";
        public static string RequiredGuarantorValidID = "Guarantor valid ID is required.";
        public static string RequiredAuthorizedValidID = "Authorized person valid ID is required.";
        public static string RequiredSPA = "Special power of attorney is required.";
        public static string RequiredDeathCert = "Deat certificate is required.";
        public static string RequiredRelativeValidID = "Relative valid ID is required.";
        public static string RequiredRemarks = "Remarks is required.";

        // 001 Not Found
        public static string NotFoundPassword = "Blocked password not found.";
        public static string NotFoundRecord = "Record not found.";
        public static string NotFoundMonthYear = "Data not found for the selected month & year.";
        public static string NotFoundGurantorWallet = "Not found guarantor wallet.";
        public static string NotFoundBonusPerformance = "Bonus performance not found.";
        public static string NotFoundBranchUserProfile = "Branch user profile not found.";
        public static string NotFoundDistributorUserProfile = "Distributor user profile not found.";

        // 001 Error
        public static string ErrorInventoryCM = "Error, Inventory already completed.";
        public static string ErrorInventoryDC = "Error, Inventory already discarded.";
        public static string ErrorActivationCM = "Error, Activation already completed.";
        public static string ErrorActivationDC = "Error, Activation already discarded.";
        public static string ErrorActivationDI = "Error, Activation already declined.";

        public static string ErrorSerialNo = "Error, Need atleast 1(one) valid serial no.";
        public static string ErrorHalfActivated = "Error, Card is half activated.";
        public static string ErrorOnProcess = "Error, Card is on process.";
        public static string UnableToRemove = "Error, Unable to remove record.";
        public static string ErrorBillingTrxnNo = "Error, Need atleast 1(one) valid billing trxn no.";

        public static string GurantorHasBalance = "Can't removed gurantor has remaining wallet balance.";
        public static string GurantorHasOutstanding = "Can't removed gurantor has outstanding balance.";
        public static string GurantorHasBilling = "Can't removed gurantor has billing balance.";
        public static string GurantorHasOverdue = "Can't removed gurantor has overdue balance.";
        public static string GurantorHasCashbond = "Can't removed gurantor has cashbond.";
        public static string NotEnoughBalance = "Insufficient remaining balance.";
        public static string OtherChargeNotConfigure = "Other charge is not configure on this branch.";
        public static string ServiceChargeNotConfigure = "Service charge is not configure on this branch.";
        public static string UnableAcceptPayment = "This branch can't accept payment for this guarantor.";
        public static string ExpirePin = "Expire card number or serial number.";
        public static string ErrorUpdateBonusAcct = "Can't update bonus guarantor.";
        public static string ErrorAddBonusAcct = "Can't add on bonus guarantor.";
        public static string ServiceChargeIDInUsed = "Can't removed service charge ID in used.";
        public static string OtherChargeIDInUsed = "Can't removed other charge ID in used.";
        public static string ProductcodeInUsed = "Can't removed product code in used.";
        public static string BranchInUsed = "Can't removed branch in used.";
        public static string CompanyInUsed = "Can't removed company in used.";
        public static string CurrencyInUsed = "Can't removed currency in used.";
        public static string DevAppInUsed = "Can't removed developer app in used.";
        public static string DistributorInUsed = "Can't removed distributor in used.";
        public static string GurantorInUsed = "Can't removed gurantor in used.";
        public static string ScheduleClassInUsed = "Can't removed schedule class in used.";
        public static string BonusSalesReqInUsed = "Can't removed bonus sale requirement in used.";
        public static string DividendInUsed = "Can't removed dividend in used.";
        public static string SalesQuotaInUsed = "Can't removed sales quota in used.";
        public static string RightCodeInUsed = "Can't removed special right in used.";
        public static string TypeCodeInUsed = "Can't removed type code in used.";
        public static string DelLockRecords = "Can't removed lock records.";
        public static string AddLockRecords = "Can't add attachment no is already locked.";

        public static string BranchUserProfileInUsed = "Can't removed branch user profile in used.";
        public static string DistributorUserProfileInUsed = "Can't removed distributor user profile in used.";

        public static string BonusPerformanceClaimed = "Bonus performance already claimed.";
        public static string BonusPerformanceApproved = "Bonus performance already approved.";
        public static string BonusPerformanceForfeited = "Bonus performance already forfeited.";
        public static string BonusPerformanceDeclined = "Bonus performance already declined.";
        public static string BonusPerformanceForApproval = "Bonus performance for approval.";
        public static string BonusPerformanceNotReleased = "Bonus performance credit can't be claimed yet.";
        public static string NoBonusAcct = "Guarantor bonus account not yet configure.";
        public static string BillSchedGenerated = "Billing schedule already generated.";

        public static string ErrorAlreadyPaid = "Can't update paid promissory note.";
        public static string ErrorNotCredits = "Can update paid by credits only.";
        public static string ErrorNotLatest = "Can update latest record only.";

        // 500
        public static string ErrorException = "Something went wrong please contact administrator.";
    }
}
