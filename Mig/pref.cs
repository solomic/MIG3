using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pref
{
    static  class pref
    {
        public static string USER = "ERUDENKO";
        public static string PASS = "ERUDENKO";
        public static string PORT = "5432";
        public static string HOST = "localhost";
        public static string DBNAME = "CMO 2017";
        public static string DEFAULT_PROGRAM = "";
        public static bool AUTH = false;
        public static int CONTACTID = 0;
        public static int CURROW = 0;
        public static string MIGDATA = @"C:\MIG DATA\";
        public static string REPORTFOLDER = @"REPORT\";
        public static string CONFIO = "";
        public static string CONNAT = "";
        public static string FULLREPORTPATCH = "";
        public static string SUBPROGRAM = "";
        //public static string POSITION = "";
        public static string NOTIFYTEMPLATE = "";
        public static string CONSTR = "";
        public static string DELEGATE = "N";
        public static int FLTCODE = 99;
        public static string INVFLTNAME = "<>";
        public static int INVWORKDAY = 20;
        public static int INV_ID;
        public static int INV_CONTACT_EDIT;
        public static string ROWACTION;

        /*СПРАВОЧНИКИ*/
        public static string FO = "SELECT  code, value FROM cmodb.lov where type='FO'  ORDER BY ord;";
        public static string FIN = "SELECT  code, value FROM cmodb.lov where type='FIN' ORDER BY ord;";
        public static string PO = "SELECT  code, value FROM cmodb.lov where type='PTEACH'  ORDER BY ord;";

        public static string FAC = "SELECT code, name FROM cmodb.facultet where status = 'Y' ORDER BY name;";
        public static string SPECLOAD = "SELECT  spec_code, name FROM cmodb.speciality where status='Y' and par_code=@param1 AND prog_teach_code=@param2 ORDER BY name;";
        public static string SEX = "SELECT code, value FROM cmodb.lov where type=@param1 order by ord;";
        //public static string NAT = "SELECT name, code  FROM cmodb.nationality order by name;";
        public static string BIRTHTOWN = "SELECT DISTINCT(birth_town) as name , 0 as code from cmodb.contact where birth_town<>'' order by name;";
        public static string NAT = "select DISTINCT(t.name) as name , 0 as code from ( " +
                " SELECT name  FROM cmodb.nationality " +
                " union " +
                " SELECT birth_country  FROM cmodb.contact " +
                " union " +
                " SELECT nationality  FROM cmodb.contact " +
                " ) t order by t.name ;";

        public static string POS = "SELECT code, value FROM cmodb.lov where type=@param1 order by ord;";
        public static string DUL = "SELECT code, value FROM cmodb.lov where type=@param1 order by ord;";
        public static string DOCTYPE = "SELECT code, value FROM cmodb.lov where type=@param1 order by ord;";
        public static string KPP = "SELECT  distinct kpp_code FROM cmodb.migr_card where kpp_code is not null and kpp_code <>'' order by kpp_code";

        //ходатайство обычное
        public static string PET_STAND1 = "SELECT  code, value FROM cmodb.lov where type='REG.HOD.ENTTITY'  ORDER BY ord;";
        public static string PET_STAND2 = "SELECT  code, value FROM cmodb.lov where type='REG.HOD.REASON'  ORDER BY ord;";
        /*СПРАВОЧНИКИ END*/

        public static string GetContactDetailSql = "SELECT contact_id,last_name, second_name, birthday, birth_town, sex, first_name, comments, "+
       " last_enu, first_enu, address_home, "+
       " cmodb.LookupValue('POS',position_code) as position, relatives, med, second_enu, delivery_dt," +
       " phone, reg_extend, nationality, type, contact_id,  birth_country ," +
       " delegate_last_name, delegate_first_name, delegate_second_name, "+
       " cmodb.LookupValue('DUL',delegate_dul_code) as delegate_dul, delegate_ser, delegate_num, delegate_dul_issue_dt, " +
       " delegate_country, delegate_nationality  , "+
        " (last_name +case when first_name is not null then ' '+first_name else '' end + case when second_name is not null then ' '+ second_name else '' end) confio,"+
        " med_from,med_to" + 
        " FROM cmodb.contact where contact_id=@param1 and status='Y';";
        public static string GetDulIdSql = "SELECT cmodb.LookupValue('DUL',type) as type, ser, num, issue, validity FROM cmodb.dul where id=@param1;";
        public static string GetDulAllSql = "SELECT id, CASE WHEN status='Y' THEN 'true' else 'false' END as dulstatus,cmodb.LookupValue('DUL',type) as \"Тип\", ser as \"Серия\", num as \"Номер\", issue as \"Выдан\", " +
            " validity as \"Годен до\", "+
            " CASE WHEN status='Y' AND validity IS NOT NULL THEN DATEADD(MONTH,-6,validity) END \"Действие с визой\"  "
            + ",updated \"Обновлено\",updated_by \"Обновил(а)\" FROM cmodb.dul where contact_id=@param1 AND deleted='N' ORDER BY CASE WHEN status IS NULL THEN 1 ELSE 0 END DESC,status DESC;";

        public static string GetMigrCardSql = "SELECT id, document_id,contact_id, ser, num, kpp_code, entry_dt, tenure_from_dt, " +
       "tenure_to_dt, status, purpose_entry/*, created, created_by, updated, "+
       "updated_by */ FROM cmodb.migr_card WHERE contact_id=@param1 ORDER BY status DESC, entry_dt DESC;";

        public static string GetDocSql = "SELECT id, code, contact_id, cmodb.LookupValue('MIGR.VIEW',type) as type,ident,  invite_num, ser, num, issue_dt, " +
       "validity_from_dt, validity_to_dt, status/*, created, created_by, "+
       "updated, updated_by*/  FROM cmodb.document where contact_id=@param1 ORDER BY status DESC;";

        public static string GetMigrCardActiveSql = "SELECT id, contact_id, ser, num, kpp_code, entry_dt, tenure_from_dt, " +
       "tenure_to_dt, status, purpose_entry/*, created, created_by, updated, " +
       "updated_by */ FROM cmodb.migr_card WHERE contact_id=@param1 AND status='Y' ORDER BY status DESC, entry_dt DESC;";

        public static string GetDocActiveSql = "SELECT id, contact_id, cmodb.LookupValue('MIGR.VIEW',type) as type,ident,  invite_num, ser, num, issue_dt, " +
       "validity_from_dt, validity_to_dt, status/*, created, created_by, " +
       "updated, updated_by*/  FROM cmodb.document where contact_id=@param1 AND status='Y'  ORDER BY status DESC;";


        public static string GetExpellActiveSql = "SELECT expelled, expelled_num, expelled_dt FROM cmodb.expell where id=@param1;";
        public static string GetExpellSql = "SELECT id, contact_id, expelled, expelled_num, expelled_dt, status  FROM cmodb.expell where contact_id=@param1 ORDER BY status DESC;";

        public static string GetAddressActiveSql = "SELECT full_address"+
                                                     " FROM cmodb.address adr"+
                                                    "  JOIN cmodb.addr_inter adrintr on (adr.code= adrintr.address_code AND adrintr.status= 'Y')"+
                                                    "  where contact_id=@param1;";
        public static string GetChildSql = "SELECT fio, CONVERT(varchar(10),birthday,104) birthday, address, id, nationality  FROM cmodb.children where contact_id=@param1;";
        public static string GetChildEditSql = "SELECT fio, birthday, address, id, nationality  FROM cmodb.children where id=@param1;";
        public static string GetAgreeActiveSql = "SELECT num, dt, from_dt, to_dt FROM cmodb.agree where id=@param1;";
        public static string GetTeachActiveSql = "SELECT postup_year,spec.code, spec.name spec_name, fac.name fac_name, cmodb.LookupValue('FO', form_teach_code) AS form_teach , cmodb.LookupValue('FIN', form_pay_code)  as form_pay, " +
                                                  " cmodb.LookupValue('PTEACH', teach.prog_teach_code) AS prog_teach, period_total, period_ind, period_total_p, period_ind_p, " +
                                                    " amount, facult_code " +
                                                  " FROM cmodb.teach_info teach"+
                                                  " left join cmodb.speciality spec on spec.spec_code=teach.spec_code"+
                                                  " left join cmodb.facultet fac on fac.code= teach.facult_code"+
                                                  " where teach.id =@param1;";
        public static string GetTeachAllSql = "SELECT case when teach.status='Y' then 'true' else 'false' end as teachstatus,teach.id,postup_year, deduct_year,cmodb.LookupValue('FO', form_teach_code) AS form_teach , cmodb.LookupValue('FIN', form_pay_code)  as form_pay, " +
                                                  " cmodb.LookupValue('PTEACH', teach.prog_teach_code) AS prog_teach, period_total, period_ind, period_total_p, period_ind_p, " +
                                                    " amount, facult_code ,spec.code, spec.name spec_name, fac.name fac_name" +
                                                  " FROM cmodb.teach_info teach" +
                                                  " left join cmodb.speciality spec on spec.spec_code=teach.spec_code" +
                                                  " left join cmodb.facultet fac on fac.code= teach.facult_code" +
                                                  " where teach.deleted= 'N' and contact_id =@param1 order by CASE WHEN teach.status IS NULL THEN 1 else 0 END DESC,teach.status DESC ;";
        public static string GetEntryActiveSql = "SELECT entry_dt, leave_dt, txt, type FROM cmodb.entry where contact_id=@param1 and status='Y';";
        public static string GetPfSql = "SELECT id,name, created, created_by FROM cmodb.pf WHERE contact_id=@param1 ORDER BY created DESC;";
        public static string GetStageSql = "SELECT id, stage, due_dt, amount, case when receipt='Y' then 'да' else 'нет' end receipt, pay_dt FROM cmodb.stage WHERE status='Y' and contact_id=@param1  ORDER BY stage;";
        public static string GetStageEditSql = "SELECT id, stage, due_dt, amount, case when receipt='Y' then 'да' else 'нет' end receipt, pay_dt FROM cmodb.stage WHERE status='Y' and contact_id=@param1 and id=@param2  ORDER BY stage;";

        public static string GetHostActiveSql = "SELECT last_name, first_name, second_name, doc, doc_num, date_issue, "+
                           " date_valid, obl, rayon, town, street, house, korp, stro, flat,  "+
                           " phone, org_name, address, org_phis, inn, created, created_by,  " +
                           " updated, updated_by, doc_ser, birthday, status " +
                                " FROM cmodb.host where id=@param1;";

        /*ПФ - общий запрос*/
        public static string PfRequest = "select "+
                    " a.last_name con_last_name, " +
                    " a.first_name con_first_name, " +
                    " a.second_name con_second_name, " +
                    " (a.last_name +case when a.first_name is not null then ' '+a.first_name else '' end + case when a.second_name is not null then ' ' + a.second_name else '' end) con_fio, " +
                    " a.last_enu con_last_enu, " +
                    " a.first_enu con_first_enu, " +
                    " a.second_enu con_second_enu, " +
                    " a.med con_med, " +
                    " CONVERT(varchar(10),a.birthday,104) con_birthday, " +
                    " a.nationality con_nat, " +
                    " a.birth_country con_birth_country, "+
                    " a.birth_town con_birth_town, "+                            
                    " cmodb.LookupValue('DUL', dul.type) dul_type, "+
                    " dul.ser dul_ser, "+
                    " dul.num dul_num, "+
                    " CONVERT(varchar(10),dul.issue,104) dul_issue, " +
                    " CONVERT(varchar(10),dul.validity,104) dul_validity, " +
                    " a.sex con_sex, "+
                    " teach.postup_year teach_postup_year, "+
                    " cmodb.LookupValue('FO', teach.form_teach_code) teach_ft, "+
                    " cmodb.LookupValue('FIN', teach.form_pay_code) teach_fp, "+
                    " cmodb.LookupValue('PTEACH', teach.prog_teach_code) teach_pt, "+
                    " spec.name spec_name, "+
                    " fac.name fac_name, "+
                    " a.comments con_comments, "+
                    " cmodb.LookupValue('POS', a.position_code) con_pos, " +
                    " CASE a.rf WHEN '1' THEN 'Да' ELSE 'Нет' END con_rf, "+
                    " COALESCE(cmodb.LookupValue('MIGR.VIEW', doc.type) ,'Безвизовая') doc_type, "+
                    " a.address_home con_address_home, "+
                    " a.relatives con_relatives, "+
                    " agr.num agr_num, "+
                    " CONVERT(varchar(10),agr.dt,104) agr_dt, " +
                    " CONVERT(varchar(10),agr.from_dt,104) agr_from_dt, " +
                    " CONVERT(varchar(10),agr.to_dt,104) agr_to_dt, " +
                    " exp.expelled exp_expelled, "+
                    " exp.expelled_num exp_num, "+
                    " CONVERT(varchar(10),exp.expelled_dt,104) exp_dt, " +
                    " b.kpp_code card_kpp, " +            
                    " b.purpose_entry card_purpose_entry, "+
                    " CONVERT(varchar(10),b.entry_dt,104) card_entry_dt, " +
                    " CONVERT(varchar(10),b.tenure_to_dt,104) card_tenure_to_dt, " +
                    " CONVERT(varchar(10),b.tenure_from_dt,104) card_tenure_from_dt, " +            
                    " b.ser card_ser, "+
                    " b.num card_num, "+
                    " CONVERT(varchar(10),doc.issue_dt,104) doc_issue_dt, " +
                    " CONVERT(varchar(10),doc.validity_to_dt,104) doc_validity_to_dt, " +
                    " CONVERT (varchar(10),DATEADD(DAY,1,doc.validity_to_dt),104) doc_validity_to_dt_1, " +
                    " doc.num doc_num, " +
                    " doc.ser doc_ser, "+
                    " doc.ident doc_ident, "+
                    " doc.invite_num doc_invite_num, "+
                    " CONVERT(varchar(10),doc.validity_from_dt,104) doc_validity_from_dt, " +
                    " c.type ent_type, "+
                    " c.entry_dt ent_entry_dt, "+
                    " c.leave_dt ent_leave_dt, "+
                    " c.txt ent_txt, "+
                    " ad.full_address ad_full_address, " +
                    " ad.obl ad_obl, " +
                    " ad.rayon ad_rayon, " +
                    " ad.town ad_town, " +
                    " ad.street ad_street, " +
                     " ad.house ad_house, " +
                    " ad.corp ad_corp, " +
                    " ad.stroenie ad_stroenie, " +
                    " ad.flat ad_flat, " +
                    " ad.socr_obl ad_socr_obl, " +
                     " ad.socr_rayon ad_socr_rayon, " +
                     " ad.socr_town ad_socr_town, " +
                     " ad.socr_street ad_socr_street, " +
                    " a.phone con_phone ," +
                    " CASE WHEN a.sex='МУЖСКОЙ' THEN 'гражданина' else 'гражданки' END gr, " +
                    " CASE WHEN a.sex='МУЖСКОЙ' THEN 'его' else 'её' END p3, " +
                    " CASE WHEN a.sex='МУЖСКОЙ' THEN 'им' else 'ею' END p4, " +
                    " a.delegate_last_name," +
                    " a.delegate_first_name " +
                    " FROM cmodb.contact a " +
                    " LEFT JOIN cmodb.migr_card b ON (a.contact_id = b.contact_id AND b.status = 'Y') "+
                    " LEFT JOIN cmodb.entry c ON (a.contact_id = c.contact_id AND c.status = 'Y') "+
                    " LEFT JOIN cmodb.addr_inter ai ON (a.contact_id = ai.contact_id AND ai.status = 'Y') LEFT JOIN cmodb.address ad ON ai.address_code = ad.code "+
                    " LEFT JOIN cmodb.teach_info teach ON (a.contact_id = teach.contact_id AND teach.status = 'Y') left join cmodb.speciality spec on spec.spec_code=teach.spec_code left join cmodb.facultet fac on fac.code= teach.facult_code "+
                    " LEFT JOIN cmodb.agree agr ON (a.contact_id = agr.contact_id AND agr.status = 'Y') "+
                    " LEFT JOIN cmodb.document doc ON (a.contact_id = doc.contact_id AND doc.status = 'Y') "+
                    " LEFT JOIN cmodb.dul dul ON (a.contact_id = dul.contact_id AND dul.status = 'Y') "+
                    " LEFT JOIN cmodb.expell exp ON (a.contact_id = exp.contact_id AND exp.status = 'Y') "+
                    " LEFT JOIN cmodb.host host ON 1=1 AND host.status='Y'" +
                    " WHERE a.status= 'Y' and a.contact_id= @param1;";
        public static string PfHost = "SELECT last_name, first_name, second_name, doc, doc_num,  CONVERT(varchar(10),date_issue,104) date_issue, " +
                                      "  CONVERT(varchar(10),date_valid,104) date_valid, obl, rayon, town, street, house, korp, stro, flat,  " +
                                     "  phone, org_name, address, org_phis, inn, created, created_by,  " +
                                      " updated, updated_by, doc_ser,  CONVERT(varchar(10),birthday,104) birthday" +
                                     " FROM cmodb.host where status='Y';";


       // public static string Stat =
       //" select ct.last_name + ' ' + ct.first_name fio, /*t.typ,*/ t.updated_by, t.action, t.contact_id from ( " +
       //"  select 'Студент 'as typ,1 as ord, contact_id,'Изменение личных данных'  as action, updated_by " +
       //"  from cmodb.contact con " +
       //"  where con.status= 'Y' AND cast(con.updated as date)= CURRENT_DATE " +
       //"   UNION ALL " +
       //"   select DISTINCT 'Документ 'as typ,2 as ord,contact_id,'Изменение регистрационных документов' as action,  updated_by " +
       //"   from cmodb.document " +
       //"   where  cast(updated as date)= CURRENT_DATE " +
       //"   UNION ALL " +
       //"   select DISTINCT 'Миграционная карта 'as typ,3 as ord,contact_id,'Изменение миграционной карты' as action, updated_by " +
       //"   from cmodb.migr_card " +
       //"   where  cast(updated as date)= CURRENT_DATE " +
       //"   UNION ALL " +
       //"   select DISTINCT 'ДУЛ' as typ,4 as ord,contact_id,'Изменение паспортных данных' as action,  updated_by " +
       //"   from cmodb.dul " +
       //"   where  cast(updated as date)= CURRENT_DATE  " +
       //"  ) t " +
       //"  JOIN cmodb.contact ct on ct.contact_id=t.contact_id " +
       //"  order by t.contact_id, t.ord ;";

    }
}
