namespace EF.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmLogTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        alarmCode = c.String(maxLength: 20, storeType: "nvarchar"),
                        alarmFlag = c.Boolean(nullable: false),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PfDetailTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pfid = c.Int(),
                        chnorder = c.Int(),
                        chnType = c.String(maxLength: 10, storeType: "nvarchar"),
                        chnName = c.String(maxLength: 10, storeType: "nvarchar"),
                        Opentime_hi = c.Single(),
                        Opentime_lo = c.Single(),
                        Closetime_hi = c.Single(),
                        Closetime_lo = c.Single(),
                        Close_pos_hi = c.Single(),
                        Close_pos_lo = c.Single(),
                        Vactime_hi = c.Single(),
                        Vactime_lo = c.Single(),
                        P_blow_hi = c.Single(),
                        P_blow_lo = c.Single(),
                        P_vac_hi = c.Single(),
                        P_vac_lo = c.Single(),
                        P_vac_pos = c.Single(),
                        P_vac_hi2 = c.Single(),
                        P_vac_lo2 = c.Single(),
                        P_vac_pos2 = c.Single(),
                        P_vac_hi3 = c.Single(),
                        P_vac_lo3 = c.Single(),
                        P_vac_pos3 = c.Single(),
                        P_vac_hi4 = c.Single(),
                        P_vac_lo4 = c.Single(),
                        P_vac_pos4 = c.Single(),
                        P_vac_hi5 = c.Single(),
                        P_vac_lo5 = c.Single(),
                        P_vac_pos5 = c.Single(),
                        auto_startpoint = c.Single(),
                        auto_endpoint = c.Single(),
                        filter_time = c.Short(),
                        Blow_Delay_time = c.Short(),
                        Blow_time = c.Short(),
                        VAC_time = c.Short(),
                        use_ch = c.Boolean(),
                        select_paiqi = c.Boolean(),
                        select_yeya = c.Boolean(),
                        select_jixie = c.Boolean(),
                        use_chuisao_M = c.Boolean(),
                        select_gauging_M = c.Boolean(),
                        select_auto_S = c.Boolean(),
                        use_VAC_time = c.Boolean(),
                        enable_Opentime = c.Boolean(),
                        enable_Closetime = c.Boolean(),
                        enable_Vactime = c.Boolean(),
                        enable_P_blow = c.Boolean(),
                        enable_Close_pos = c.Boolean(),
                        enable_P_vac = c.Boolean(),
                        enable_P_vac2 = c.Boolean(),
                        enable_P_vac3 = c.Boolean(),
                        enable_P_vac4 = c.Boolean(),
                        enable_P_vac5 = c.Boolean(),
                        auto_startpoint2 = c.Single(),
                        auto_startpoint3 = c.Single(),
                        auto_endpoint2 = c.Single(),
                        auto_endpoint3 = c.Single(),
                        P_blow_hi2 = c.Single(),
                        P_blow_lo2 = c.Single(),
                        P_blow_hi3 = c.Single(),
                        P_blow_lo3 = c.Single(),
                        set_CheckPoint_C = c.Single(),
                        set_CheckPointHi_C = c.Single(),
                        set_CheckPointLo_C = c.Single(),
                        VAC_time2 = c.Short(),
                        VAC_time3 = c.Short(),
                        Blow_INR_time = c.Short(),
                        Blow_time2 = c.Short(),
                        Blow_time3 = c.Short(),
                        use_blow = c.Boolean(),
                        enable_P_blow2 = c.Boolean(),
                        enable_P_blow3 = c.Boolean(),
                        enable_zu1 = c.Boolean(),
                        enable_zu2 = c.Boolean(),
                        enable_zu3 = c.Boolean(),
                        use_VAC_hemu = c.Boolean(),
                        use_VAC_stop = c.Boolean(),
                        use_checkPoint_C = c.Boolean(),
                        update_time = c.DateTime(nullable: false, precision: 0),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PfLogTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        useraccount = c.String(maxLength: 200, storeType: "nvarchar"),
                        pfname = c.String(unicode: false),
                        logType = c.Int(nullable: false),
                        logData = c.String(maxLength: 200, storeType: "nvarchar"),
                        logBefore = c.String(maxLength: 200, storeType: "nvarchar"),
                        logAfter = c.String(maxLength: 200, storeType: "nvarchar"),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PfMainTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creat_useraccount = c.String(maxLength: 200, storeType: "nvarchar"),
                        update_useraccount = c.String(maxLength: 200, storeType: "nvarchar"),
                        pfname = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        update_time = c.DateTime(nullable: false, precision: 0),
                        create_time = c.DateTime(nullable: false, precision: 0),
                        validity = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SampleTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        use_pf_id = c.Int(nullable: false),
                        pg1 = c.String(unicode: false),
                        pg2 = c.String(unicode: false),
                        pg3 = c.String(unicode: false),
                        pg4 = c.String(unicode: false),
                        pg5 = c.String(unicode: false),
                        pg6 = c.String(unicode: false),
                        pg7 = c.String(unicode: false),
                        pg8 = c.String(unicode: false),
                        pg9 = c.String(unicode: false),
                        pg10 = c.String(unicode: false),
                        pg11 = c.String(unicode: false),
                        pg12 = c.String(unicode: false),
                        pg13 = c.String(unicode: false),
                        pg14 = c.String(unicode: false),
                        pg15 = c.String(unicode: false),
                        pg16 = c.String(unicode: false),
                        pg17 = c.String(unicode: false),
                        pg18 = c.String(unicode: false),
                        pg19 = c.String(unicode: false),
                        pg20 = c.String(unicode: false),
                        pos = c.String(unicode: false),
                        vel = c.String(unicode: false),
                        startpoint = c.String(maxLength: 500, storeType: "nvarchar"),
                        endpoint = c.String(maxLength: 500, storeType: "nvarchar"),
                        chuisaoyali = c.String(maxLength: 500, storeType: "nvarchar"),
                        duoduanjiance1 = c.String(maxLength: 500, storeType: "nvarchar"),
                        duoduanjiance2 = c.String(maxLength: 500, storeType: "nvarchar"),
                        duoduanjiance3 = c.String(maxLength: 500, storeType: "nvarchar"),
                        duoduanjiance4 = c.String(maxLength: 500, storeType: "nvarchar"),
                        duoduanjiance5 = c.String(maxLength: 500, storeType: "nvarchar"),
                        fanying = c.String(maxLength: 500, storeType: "nvarchar"),
                        tongfeng = c.String(maxLength: 500, storeType: "nvarchar"),
                        fengbi = c.String(maxLength: 500, storeType: "nvarchar"),
                        start_yeya_PG = c.String(maxLength: 500, storeType: "nvarchar"),
                        stop_yeya_PG = c.String(maxLength: 500, storeType: "nvarchar"),
                        start_PG_tank = c.String(maxLength: 500, storeType: "nvarchar"),
                        end_PG_tank = c.String(maxLength: 500, storeType: "nvarchar"),
                        CA_PG = c.Single(),
                        zongchouqi = c.Single(),
                        usechanle = c.String(maxLength: 100, storeType: "nvarchar"),
                        pfname = c.String(maxLength: 100, storeType: "nvarchar"),
                        DCM_id = c.String(maxLength: 100, storeType: "nvarchar"),
                        product_id = c.String(maxLength: 100, storeType: "nvarchar"),
                        select_paiqi = c.String(maxLength: 100, storeType: "nvarchar"),
                        select_yeya = c.String(maxLength: 100, storeType: "nvarchar"),
                        select_jixie = c.String(maxLength: 100, storeType: "nvarchar"),
                        duoduanjiance1_hmienable = c.String(maxLength: 100, storeType: "nvarchar"),
                        duoduanjiance2_hmienable = c.String(maxLength: 100, storeType: "nvarchar"),
                        duoduanjiance3_hmienable = c.String(maxLength: 100, storeType: "nvarchar"),
                        duoduanjiance4_hmienable = c.String(maxLength: 100, storeType: "nvarchar"),
                        duoduanjiance5_hmienable = c.String(maxLength: 100, storeType: "nvarchar"),
                        update_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        useraccount = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        userpassword = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        userrole = c.Int(nullable: false),
                        validity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTable");
            DropTable("dbo.SampleTable");
            DropTable("dbo.PfMainTable");
            DropTable("dbo.PfLogTable");
            DropTable("dbo.PfDetailTable");
            DropTable("dbo.AlarmLogTable");
        }
    }
}
