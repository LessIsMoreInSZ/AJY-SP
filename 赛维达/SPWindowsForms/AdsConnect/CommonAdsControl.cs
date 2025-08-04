using DocumentFormat.OpenXml.InkML;
using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect.TwincateStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace SPWindowsForms.AdsConnect
{
    public class CommonAdsControl
    {
        private TcAdsClient adsClient;
        private string netID;
        private int port;
        public readonly string st_sample_name = ".SQL";
        // public readonly string st_ysgjz_name = ".YSG";
        public readonly string st_recip_name = ".st_recip";
        public readonly string st_recip_c1_name = ".st_recip_c1";
        public readonly string st_recip_e1_name = ".st_recip_e1";
        public readonly string hmi_st_layer_name = ".Status";
        public readonly string st_alarm_D_name = ".hmi_alarmout_D";
        public readonly string st_alarm_C_name = ".hmi_alarmout_C";
        public readonly string st_alarm_E_name = ".hmi_alarmout_E";
        public readonly string hmi_D_name = ".hmi_D";
        public readonly string hmi_C_name = ".hmi_C";
        public readonly string hmi_E_name = ".hmi_E";
        public readonly string hmi_test_name = ".hmi_test";
        public readonly string hmi_auto_name = ".hmi_auto";
        public readonly string st_ups_name = ".UPS";
        public readonly string hmi_Vacsource_name = ".hmi_Vacsource";
        public readonly string hmi_IO_name = ".hmi_IO";
        public readonly string hmi_K_name = ".hmi_K";
        public readonly string hmi_butt_save_name = ".hmi_butt_save";

        public readonly string enable_ManAction_name = ".enable_ManAction";
        public readonly string hmi_butt_ManPUMP_name = ".hmi_butt_ManPUMP";
        public readonly string pump_timing_name = ".pump_timing";
        public readonly string hmi_feedback_kaidu_name = ".hmi_feedback_kaidu";
        public readonly string st_Pg_name = ".UPS_PG";

        public int totalChnCount = 0;
        public readonly int plcD_count = 10;
        public readonly int plcC_count = 1;
        public readonly int plcE_count = 1;
        // public Dictionary<string,List<>>
        public CommonAdsControl(string _netID, int _port)
        {
            try
            {
                adsClient = new TcAdsClient();
                netID = _netID;
                port = _port;
                adsClient.Connect(netID, port);
                totalChnCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count + GlobalVar.systemSetting.e_chn_count;
            }
            catch (Exception ex)
            {
                throw new Exception("CommonAdsControl::" + ex.Message);
            }
        }
        public void CheckAdsConnection()
        {
            try
            {
                if (!adsClient.IsConnected)
                {
                    adsClient.Connect(netID, port);
                }
                if (!adsClient.IsConnected)
                {
                    throw new Exception("CheckAdsConnection::Can't connect.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CheckAdsConnection::" + ex.Message);
            }
        }
        #region sample
        public void Writeenablereading()
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_sample_name);
                int datastream_count = 1;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = 0;
                binwrite.Write(false);
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception("Readenablereading()" + ex.Message);
            }
        }
        public bool Readenablereading()
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_sample_name);
                int datastream_count = 1;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
                return binread.ReadBoolean();
            }
            catch (Exception ex)
            {
                throw new Exception("Readenablereading()" + ex.Message);
            }
        }
        public st_sample Readst_sample(string pfname = "")
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_sample_name);
                //var test= adsClient.ReadAny(_sampleHand, typeof(bool)).ToString();
                var _sample = new st_sample();
                int totalChnCount = GlobalVar.systemSetting.sample_chncount;
                #region Ini _sample
                _sample.pg1 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg2 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg3 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg4 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg5 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg6 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg7 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg8 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg9 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pg10 = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.pos = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.vel = new float[GlobalVar.systemSetting.sample_pointcount];
                _sample.startpoint = new float[totalChnCount];
                _sample.endpoint = new float[totalChnCount];
                _sample.chuisaoyali = new float[totalChnCount];
                _sample.duoduanjiance1 = new float[totalChnCount];
                _sample.duoduanjiance2 = new float[totalChnCount];
                _sample.duoduanjiance3 = new float[totalChnCount];
                _sample.duoduanjiance4 = new float[totalChnCount];
                _sample.duoduanjiance5 = new float[totalChnCount];
                _sample.fanying = new float[totalChnCount];
                _sample.tongfeng = new float[totalChnCount];
                _sample.fengbi = new float[totalChnCount];
                _sample.start_yeya_PG = new float[totalChnCount];
                _sample.stop_yeya_PG = new float[totalChnCount];

                _sample.start_PG_tank = new float[2];
                _sample.end_PG_tank = new float[2];
                _sample.usechanle = new bool[totalChnCount];
                _sample.pg11 = new float[GlobalVar.systemSetting.sample_pointcount];

                #endregion

                #region set datastream_count
                int datastream_count = 1;//enablereading

                //datastream_count += GlobalVar.systemSetting.sample_pointcount * 4 * 12;//pg1~vel real
                datastream_count += GlobalVar.systemSetting.sample_pointcount * 4 * 13;//pg1~vel real

                datastream_count += totalChnCount * 4 * 13;//startpoint~stop_yeya_PG
                datastream_count += 2 * 4 * 2;//start_PG_tank~end_PG_tank
                datastream_count += 4;//CA_PG
                datastream_count += 4;//zongchouqi
                datastream_count += totalChnCount * 1;//usechanle
                                                      // datastream_count += 16 + 1;//pfname
                datastream_count += 50 + 1;//DCM_id
                #endregion
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);
                #region ReadAll
                _sample.enablereading = binread.ReadBoolean();
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg1[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg2[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg3[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg4[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg5[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg6[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg7[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg8[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg9[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg10[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pos[i] = binread.ReadSingle();
                }
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.vel[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.startpoint[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.endpoint[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.chuisaoyali[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.duoduanjiance1[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.duoduanjiance2[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.duoduanjiance3[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.duoduanjiance4[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.duoduanjiance5[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.fanying[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.tongfeng[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.fengbi[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.start_yeya_PG[i] = binread.ReadSingle();
                }
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.stop_yeya_PG[i] = binread.ReadSingle();
                }
                for (int i = 0; i < 2; i++)
                {
                    _sample.start_PG_tank[i] = binread.ReadSingle();
                }
                for (int i = 0; i < 2; i++)
                {
                    _sample.end_PG_tank[i] = binread.ReadSingle();
                }
                _sample.CA_PG = binread.ReadSingle();
                _sample.zongchouqi = binread.ReadSingle();
                for (int i = 0; i < totalChnCount; i++)
                {
                    _sample.usechanle[i] = binread.ReadBoolean();
                }
                //_sample.pfname = new string(binread.ReadChars(16 + 1)).PLCString();
                _sample.pfname = pfname;

                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    _sample.pg11[i] = binread.ReadSingle();
                }
                //_sample.DCM_id = new string(binread.ReadChars(50 + 1)).PLCString();
                _sample.DCM_id = new string(binread.ReadChars(55)).PLCString();

                
                #endregion
                adsClient.DeleteVariableHandle(hvar);
                return _sample;
            }
            catch (Exception ex)
            {
                throw new Exception("Readst_sample()" + ex.Message);
            }
        }
        #endregion

        #region pf
        public void WriteSTAll(List<PfDetailTable> pfs)
        {
            var pfds = pfs.Where(m => m.chnType == "D").OrderBy(m => m.chnorder).ToList();
            if (pfds.Count > 0)
                WriteST_recipe(pfds);
            var pfcs = pfs.Where(m => m.chnType == "C").OrderBy(m => m.chnorder).ToList();
            if (pfcs.Count > 0)
                Writest_recipe_C1(pfcs);
            var pfes = pfs.Where(m => m.chnType == "E").OrderBy(m => m.chnorder).ToList();
            if (pfes.Count > 0)
                Writest_recipe_E1(pfes);
        }
        public void WriteST_recipe(List<PfDetailTable> pfs)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_recip_name);
                int datastream_count = 4 * 27 + 1 * 18 + 2 * 4;
                datastream_count = datastream_count * GlobalVar.systemSetting.d_chn_count;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = 0;

                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {

                    var pf = pfs.FirstOrDefault(m => m.chnorder == i + 1);
                    if (pf == null) continue;
                    binwrite.Write(pf.Opentime_hi ?? 0);
                    binwrite.Write(pf.Opentime_lo ?? 0);

                    binwrite.Write(pf.Closetime_hi ?? 0);
                    binwrite.Write(pf.Closetime_lo ?? 0);

                    binwrite.Write(pf.Close_pos_hi ?? 0);
                    binwrite.Write(pf.Close_pos_lo ?? 0);

                    binwrite.Write(pf.Vactime_hi ?? 0);
                    binwrite.Write(pf.Vactime_lo ?? 0);

                    binwrite.Write(pf.P_blow_hi ?? 0);
                    binwrite.Write(pf.P_blow_lo ?? 0);

                    binwrite.Write(pf.P_vac_hi ?? 0);
                    binwrite.Write(pf.P_vac_hi2 ?? 0);
                    binwrite.Write(pf.P_vac_hi3 ?? 0);
                    binwrite.Write(pf.P_vac_hi4 ?? 0);
                    binwrite.Write(pf.P_vac_hi5 ?? 0);

                    binwrite.Write(pf.P_vac_lo ?? 0);
                    binwrite.Write(pf.P_vac_lo2 ?? 0);
                    binwrite.Write(pf.P_vac_lo3 ?? 0);
                    binwrite.Write(pf.P_vac_lo4 ?? 0);
                    binwrite.Write(pf.P_vac_lo5 ?? 0);

                    binwrite.Write(pf.P_vac_pos ?? 0);
                    binwrite.Write(pf.P_vac_pos2 ?? 0);
                    binwrite.Write(pf.P_vac_pos3 ?? 0);
                    binwrite.Write(pf.P_vac_pos4 ?? 0);
                    binwrite.Write(pf.P_vac_pos5 ?? 0);


                    binwrite.Write(pf.auto_startpoint ?? 0);
                    binwrite.Write(pf.auto_endpoint ?? 0);

                    binwrite.Write(pf.filter_time ?? 0);
                    binwrite.Write(pf.Blow_Delay_time ?? 0);
                    binwrite.Write(pf.Blow_time ?? 0);
                    binwrite.Write(pf.VAC_time ?? 0);

                    binwrite.Write(pf.use_ch ?? false);
                    binwrite.Write(pf.select_paiqi ?? false);
                    binwrite.Write(pf.select_yeya ?? false);
                    binwrite.Write(pf.select_jixie ?? false);
                    binwrite.Write(pf.use_chuisao_M ?? false);
                    binwrite.Write(pf.select_gauging_M ?? false);
                    binwrite.Write(pf.select_auto_S ?? false);
                    binwrite.Write(pf.use_VAC_time ?? false);


                    binwrite.Write(pf.enable_Opentime ?? false);
                    binwrite.Write(pf.enable_Closetime ?? false);
                    binwrite.Write(pf.enable_Vactime ?? false);
                    binwrite.Write(pf.enable_P_blow ?? false);
                    binwrite.Write(pf.enable_Close_pos ?? false);
                    binwrite.Write(pf.enable_P_vac ?? false);
                    binwrite.Write(pf.enable_P_vac2 ?? false);
                    binwrite.Write(pf.enable_P_vac3 ?? false);
                    binwrite.Write(pf.enable_P_vac4 ?? false);
                    binwrite.Write(pf.enable_P_vac5 ?? false);

                }
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception("WriteST_recipe()" + ex.Message);
            }
        }
        public void Writest_recipe_C1(List<PfDetailTable> pfs)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_recip_c1_name);
                int datastream_count = 4 * 19 + 1 * 12 + 2 * 8;
                datastream_count = datastream_count * GlobalVar.systemSetting.c_chn_count;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = 0;

                for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                {

                    var pf = pfs.FirstOrDefault(m => m.chnorder == i + 1);
                    if (pf == null) continue;
                    binwrite.Write(pf.auto_startpoint ?? 0);
                    binwrite.Write(pf.auto_startpoint2 ?? 0);
                    binwrite.Write(pf.auto_startpoint3 ?? 0);

                    binwrite.Write(pf.auto_endpoint ?? 0);
                    binwrite.Write(pf.auto_endpoint2 ?? 0);
                    binwrite.Write(pf.auto_endpoint3 ?? 0);

                    binwrite.Write(pf.P_vac_hi ?? 0);
                    binwrite.Write(pf.P_vac_lo ?? 0);

                    binwrite.Write(pf.Vactime_hi ?? 0);
                    binwrite.Write(pf.Vactime_lo ?? 0);

                    binwrite.Write(pf.P_blow_hi ?? 0);
                    binwrite.Write(pf.P_blow_lo ?? 0);

                    binwrite.Write(pf.P_blow_hi2 ?? 0);
                    binwrite.Write(pf.P_blow_lo2 ?? 0);

                    binwrite.Write(pf.P_blow_hi3 ?? 0);
                    binwrite.Write(pf.P_blow_lo3 ?? 0);

                    binwrite.Write(pf.set_CheckPoint_C ?? 0);
                    binwrite.Write(pf.set_CheckPointHi_C ?? 0);
                    binwrite.Write(pf.set_CheckPointLo_C ?? 0);

                    binwrite.Write((short)(pf.VAC_time ?? 0));
                    binwrite.Write((short)(pf.VAC_time2 ?? 0));
                    binwrite.Write((short)(pf.VAC_time3 ?? 0));

                    binwrite.Write((short)(pf.Blow_Delay_time ?? 0));
                    binwrite.Write((short)(pf.Blow_INR_time ?? 0));
                    binwrite.Write((short)(pf.Blow_time ?? 0));
                    binwrite.Write((short)(pf.Blow_time2 ?? 0));
                    binwrite.Write((short)(pf.Blow_time3 ?? 0));


                    binwrite.Write(pf.use_ch ?? false);
                    binwrite.Write(pf.use_blow ?? false);
                    binwrite.Write(pf.use_VAC_time ?? false);
                    binwrite.Write(pf.use_checkPoint_C ?? false);

                    binwrite.Write(pf.enable_Vactime ?? false);
                    binwrite.Write(pf.enable_P_vac ?? false);
                    binwrite.Write(pf.enable_P_blow ?? false);
                    binwrite.Write(pf.enable_P_blow2 ?? false);
                    binwrite.Write(pf.enable_P_blow3 ?? false);
                    binwrite.Write(pf.enable_zu1 ?? false);
                    binwrite.Write(pf.enable_zu2 ?? false);
                    binwrite.Write(pf.enable_zu3 ?? false);

                }
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception("Writest_recipe_C1()" + ex.Message);
            }
        }
        public void Writest_recipe_E1(List<PfDetailTable> pfs)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_recip_e1_name);
                int datastream_count = 4 * 2 + 1 * 4;
                datastream_count = datastream_count * GlobalVar.systemSetting.e_chn_count;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = 0;
                for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                {
                    var pf = pfs.FirstOrDefault(m => m.chnorder == i + 1);
                    if (pf == null) continue;
                    binwrite.Write(pf.P_vac_hi ?? 0);
                    binwrite.Write(pf.P_vac_lo ?? 0);

                    binwrite.Write(pf.use_ch ?? false);
                    binwrite.Write(pf.use_VAC_hemu ?? false);
                    binwrite.Write(pf.use_VAC_stop ?? false);
                    binwrite.Write(pf.enable_P_vac ?? false);

                }
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception("Writest_recipe_E1()" + ex.Message);
            }
        }
        #endregion

        #region pf 实时值
        public st_alarm_D[] ReadSt_alarm_D()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(st_alarm_D_name);
            var _st_alarm_Ds = new st_alarm_D[GlobalVar.systemSetting.d_chn_count];
            int datastream_count = 10 * 4 * GlobalVar.systemSetting.d_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);

            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                _st_alarm_Ds[i].P_vac_Pvout = new float[5];
                _st_alarm_Ds[i].Opentime_Pvout = binread.ReadSingle();
                _st_alarm_Ds[i].Closetime_Pvout = binread.ReadSingle();
                _st_alarm_Ds[i].Close_pos_Pvout = binread.ReadSingle();
                _st_alarm_Ds[i].Vactime_Pvout = binread.ReadSingle();
                _st_alarm_Ds[i].P_blow_Pvout = binread.ReadSingle();
                for (int j = 0; j < 5; j++)
                {
                    _st_alarm_Ds[i].P_vac_Pvout[j] = binread.ReadSingle();
                }
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_alarm_Ds;
        }
        public st_alarm_C[] ReadSt_alarm_C()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(st_alarm_C_name);
            var _st_alarm_Cs = new st_alarm_C[GlobalVar.systemSetting.c_chn_count];
            int datastream_count = 6 * 4 * GlobalVar.systemSetting.c_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                _st_alarm_Cs[i].Vactime_Pvout = binread.ReadSingle();
                _st_alarm_Cs[i].P_vac_Pvout = binread.ReadSingle();
                _st_alarm_Cs[i].P_blow1_Pvout = binread.ReadSingle();
                _st_alarm_Cs[i].P_blow2_Pvout = binread.ReadSingle();
                _st_alarm_Cs[i].P_blow3_Pvout = binread.ReadSingle();
                _st_alarm_Cs[i].P_CheckPoint_Pvout = binread.ReadSingle();
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_alarm_Cs;
        }
        public st_alarm_E[] ReadSt_alarm_E()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(st_alarm_E_name);
            var _st_alarm_Es = new st_alarm_E[GlobalVar.systemSetting.e_chn_count];
            int datastream_count = 4 * GlobalVar.systemSetting.e_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                _st_alarm_Es[i].P_vac_Pvout = binread.ReadSingle();
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_alarm_Es;
        }
        #endregion

        #region 通道概览
        public st_hmiTB_D[] Readst_hmiTB_D()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_D_name);
            var _st_hmiTB_Ds = new st_hmiTB_D[GlobalVar.systemSetting.d_chn_count];
            int datastream_count = (16 * 1 + 4 * 4 + 9) * GlobalVar.systemSetting.d_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);

            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                _st_hmiTB_Ds[i].led_vy = new bool[9];
                _st_hmiTB_Ds[i].pg = new float[5];
                _st_hmiTB_Ds[i].hmi_butt_vy = new bool[9];
                _st_hmiTB_Ds[i].butt_link_Vac = binread.ReadBoolean();
                _st_hmiTB_Ds[i].butt_link_Blow = binread.ReadBoolean();
                _st_hmiTB_Ds[i].butt_Vac = binread.ReadBoolean();
                _st_hmiTB_Ds[i].butt_Blow = binread.ReadBoolean();
                _st_hmiTB_Ds[i].butt_open = binread.ReadBoolean();
                _st_hmiTB_Ds[i].butt_close = binread.ReadBoolean();
                _st_hmiTB_Ds[i].status_ls = binread.ReadBoolean();

                for (int j = 0; j < 9; j++)
                {
                    _st_hmiTB_Ds[i].led_vy[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmiTB_Ds[i].pg[j] = binread.ReadSingle();
                }
                for (int j = 0; j < 9; j++)
                {
                    _st_hmiTB_Ds[i].hmi_butt_vy[j] = binread.ReadBoolean();
                }
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_hmiTB_Ds;
        }
        public st_hmiTB_C[] Readst_hmiTB_C()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_C_name);
            var _st_hmiTB_Cs = new st_hmiTB_C[GlobalVar.systemSetting.c_chn_count];
            int datastream_count = (10 * 1 + 2 * 4 + 8) * GlobalVar.systemSetting.c_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);

            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                _st_hmiTB_Cs[i].led_vy = new bool[8];
                _st_hmiTB_Cs[i].pg = new float[2];
                _st_hmiTB_Cs[i].hmi_butt_vy = new bool[8];
                _st_hmiTB_Cs[i].butt_link_Vac = binread.ReadBoolean();
                _st_hmiTB_Cs[i].butt_link_Blow = binread.ReadBoolean();

                for (int j = 0; j < 8; j++)
                {
                    _st_hmiTB_Cs[i].led_vy[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 2; j++)
                {
                    _st_hmiTB_Cs[i].pg[j] = binread.ReadSingle();
                }
                for (int j = 0; j < 8; j++)
                {
                    _st_hmiTB_Cs[i].hmi_butt_vy[j] = binread.ReadBoolean();
                }
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_hmiTB_Cs;
        }
        public st_hmiTB_E[] Readst_hmiTB_E()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_E_name);
            var _st_hmiTB_Es = new st_hmiTB_E[GlobalVar.systemSetting.e_chn_count];
            int datastream_count = (2 * 1 + 4 + 1) * GlobalVar.systemSetting.e_chn_count;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                _st_hmiTB_Es[i].butt_link_Vac = binread.ReadBoolean();
                _st_hmiTB_Es[i].led_vy1 = binread.ReadBoolean();
                _st_hmiTB_Es[i].pg1 = binread.ReadSingle();
                _st_hmiTB_Es[i].hmi_butt_vy1 = binread.ReadBoolean();
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_hmiTB_Es;
        }
        #endregion
        #region read top status
        public hmi_st_layer Readhmi_st_layer()
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(hmi_st_layer_name);
                //var test= adsClient.ReadAny(_sampleHand, typeof(bool)).ToString();
                var _hmi_st_layer = new hmi_st_layer();
                int _tank_count = 2;
                int _d_count = 10;
                int _c_count = 2;
                int _e_count = 2;



                _hmi_st_layer.P_Tank = new float[_tank_count];  //1#、2#储气罐压力*)

                _hmi_st_layer.Use_ch_E = new bool[_e_count]; // 顶针通道使用E1-E2*)
                _hmi_st_layer.Use_ch_C = new bool[_c_count]; // 料筒通道使用C1-C2*)
                _hmi_st_layer.Use_ch_D = new bool[_d_count]; // 模腔通道使用D1-D10*)
                int datastream_count = 31 + 1 * (14 + _e_count + _c_count + _d_count) + 4 * (2 + _tank_count);

                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);
                #region ReadAll

                _hmi_st_layer.DcmClose = binread.ReadBoolean();               // 压铸机合模到位*)
                _hmi_st_layer.DcmOpen = binread.ReadBoolean();                // 压铸机开模到位*)
                _hmi_st_layer.DcmAuto = binread.ReadBoolean();                // 压铸机自动信号*)
                _hmi_st_layer.DcmHotMould = binread.ReadBoolean();            // 压铸机热模信号*)
                _hmi_st_layer.DcmBlow = binread.ReadBoolean();                // 压铸机吹扫信号/顶针前限*)
                _hmi_st_layer.DcmPosZero = binread.ReadBoolean();         // 压铸机压射零位*)
                _hmi_st_layer.DcmPos = binread.ReadSingle();                // 压铸机压射位置*)
                _hmi_st_layer.DcmVel = binread.ReadSingle();                 // 压铸机压射速度*)

                _hmi_st_layer.Zero_Calibration = binread.ReadBoolean();
                _hmi_st_layer.Es_pump = binread.ReadBoolean();
                _hmi_st_layer.Es_Controls = binread.ReadBoolean();
                _hmi_st_layer.Status_auto = binread.ReadBoolean();
                _hmi_st_layer.Status_err = binread.ReadBoolean();
                _hmi_st_layer.Status_ready = binread.ReadBoolean();
                _hmi_st_layer.Status_pumpruning = binread.ReadBoolean();
                for (int i = 0; i < _tank_count; i++)
                {
                    _hmi_st_layer.P_Tank[i] = binread.ReadSingle();
                }
                for (int i = 0; i < _e_count; i++)
                {
                    _hmi_st_layer.Use_ch_E[i] = binread.ReadBoolean();
                }
                for (int i = 0; i < _c_count; i++)
                {
                    _hmi_st_layer.Use_ch_C[i] = binread.ReadBoolean();
                }
                for (int i = 0; i < _d_count; i++)
                {
                    _hmi_st_layer.Use_ch_D[i] = binread.ReadBoolean();
                }
                _hmi_st_layer.Status_remote_pump = binread.ReadBoolean();
                _hmi_st_layer.DcmPosCoding = new string(binread.ReadChars(30 + 1)).PLCString(); // 压铸机压射编码/刻印码*)
                #endregion
                adsClient.DeleteVariableHandle(hvar);
                return _hmi_st_layer;
            }
            catch (Exception ex)
            {
                throw new Exception("Readhmi_st_layer()" + ex.Message);
            }
        }
        #endregion

        #region common read and write
        public ushort[] ReadCommonUShort(string varname, int count = 1)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = count * 2;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = binread.ReadUInt16();
                }
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonReal()::" + ex.Message);
            }
        }
        public ushort ReadCommonUShort2(string varname)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 2;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                ushort result;
                result = binread.ReadUInt16();
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonReal()::" + ex.Message);
            }
        }
        public short[] ReadCommonShort(string varname, int count = 1)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = count * 2;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new short[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = binread.ReadInt16();
                }
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonReal()::" + ex.Message);
            }
        }
        public bool[] ReadCommonBool(string varname, int count = 1)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = count;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new bool[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = binread.ReadBoolean();
                }
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonBool()::" + ex.Message);
            }
        }
        public bool ReadCommonBool2(string varname)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 1;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = binread.ReadBoolean();
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonBool2()::" + ex.Message);
            }
        }
        public float[] ReadCommonReal(string varname, int count = 1)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = count * 4;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new float[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = binread.ReadSingle();
                }
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonReal()::" + ex.Message);
            }
        }
        public float ReadCommonReal2(string varname)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 4;//enablereading
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = binread.ReadSingle();
                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonBool2()::" + ex.Message);
            }
        }
        public void WriteCommonBool(string varname, int parPos, bool value, int arrayOrder = 0, int arrayTotalLength = 0)
        {
            try
            {
                //   Console.WriteLine(varname + value.ToString() + "\r\n");
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 1 + arrayOrder * arrayTotalLength + parPos;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = arrayOrder * arrayTotalLength + parPos;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonBool()::" + ex.Message);
            }
        }
        public void WriteCommonBool2(string varname, bool value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 1;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = 0;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonBool2()::" + ex.Message);
            }
        }
        public void WriteCommonBoolByBefore(string varname, int beforeCount, bool value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 1 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonBoolByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonBoolByBefore2(string varname, int beforeCount, bool value1, bool value2)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 2 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value1);
                binwrite.Write(value2);
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonBoolByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonBoolByBefore3(string varname, int beforeCount, bool value1, bool value2, bool value3)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 3 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value1);
                binwrite.Write(value2);
                binwrite.Write(value3);
                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonBoolByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonShortByBefore(string varname, int beforeCount, short value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 2 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonShortByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonUShortByBefore(string varname, int beforeCount, ushort value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 2 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonUShortByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonFloatByBefore(string varname, int beforeCount, float value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 4 + beforeCount;
                AdsStream datastream = new AdsStream(datastream_count);

                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                BinaryWriter binwrite = new BinaryWriter(datastream);
                datastream.Position = beforeCount;
                binwrite.Write(value);

                adsClient.Write(hvar, datastream);
                adsClient.DeleteVariableHandle(hvar);
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonShortByBefore()::" + ex.Message);
            }
        }
        public void WriteCommonString(string varname, int length, string value)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);

                adsClient.WriteAny(hvar, value, new int[] { length });
                adsClient.DeleteVariableHandle(hvar);


            }
            catch (Exception ex)
            {
                throw new Exception(varname + " WriteCommonString()::" + ex.Message);
            }
        }
        public st_Pg ReadStPg()
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(st_Pg_name);
                int datastream_count = 64 * 10 + 32 * GlobalVar.systemSetting.c_chn_count + 16 * GlobalVar.systemSetting.c_chn_count + 48;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new st_Pg();
                int d_count = GlobalVar.systemSetting.d_chn_count < 10 ? 10 : GlobalVar.systemSetting.d_chn_count;

                result.pg_D = new PGs_D[d_count/*10*/];

                for (int i = 0; i < d_count/*10*/; i++)
                {
                    result.pg_D[i] = new PGs_D();
                    result.pg_D[i].P_Vac = ReadPgs(result.pg_D[i].P_Vac, binread);
                    result.pg_D[i].P_Blow = ReadPgs(result.pg_D[i].P_Blow, binread);
                    result.pg_D[i].P_M = ReadPgs(result.pg_D[i].P_M, binread);
                    result.pg_D[i].P_HYD = ReadPgs(result.pg_D[i].P_HYD, binread);
                }
                if (GlobalVar.systemSetting.c_chn_count > 0)
                {
                    result.pg_C = new PGs_C[GlobalVar.systemSetting.c_chn_count];
                    for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                    {
                        result.pg_C[i] = new PGs_C();
                        result.pg_C[i].P_Vac = ReadPgs(result.pg_C[i].P_Vac, binread);
                        result.pg_C[i].P_Blow = ReadPgs(result.pg_C[i].P_Blow, binread);
                    }
                }
                if (GlobalVar.systemSetting.e_chn_count > 0)
                {
                    result.PG_E = new PGs_Vac[GlobalVar.systemSetting.e_chn_count];
                    for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                    {
                        result.PG_E[i] = ReadPgVacs(result.PG_E[i], binread);
                    }
                }
                result.PG_Tank1 = ReadPgVacs(result.PG_Tank1, binread);
                result.PG_Tank2 = ReadPgVacs(result.PG_Tank2, binread);
                result.PG_air = ReadPgVacs(result.PG_air, binread);

                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" ReadStPg()::" + ex.Message);
            }
        }
        public PGs_Vac ReadPgVacs(PGs_Vac result, BinaryReader binread)
        {
            result = new PGs_Vac();
            result.P_Vac = ReadPgs(result.P_Vac, binread);
            return result;
        }
        public PGs ReadPgs(PGs result, BinaryReader binread)
        {
            result = new PGs();
            result.in_pg = binread.ReadInt16();

            result.hi_pg = binread.ReadSingle();

            result.lo_pg = binread.ReadSingle();
            result.out_pg = binread.ReadSingle();

            result.out_hmipg = binread.ReadInt16();
            return result;
        }
        public PGs ReadCommonPgs(string varname)
        {
            try
            {
                CheckAdsConnection();
                int hvar = new int();
                hvar = adsClient.CreateVariableHandle(varname);
                int datastream_count = 16;
                AdsStream datastream = new AdsStream(datastream_count);
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;
                adsClient.Read(hvar, datastream);

                var result = new PGs();


                result.in_pg = binread.ReadInt16();

                result.hi_pg = binread.ReadSingle();

                result.lo_pg = binread.ReadSingle();
                result.out_pg = binread.ReadSingle();

                result.out_hmipg = binread.ReadInt16();

                adsClient.DeleteVariableHandle(hvar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(varname + " ReadCommonPgs()::" + ex.Message);
            }
        }
        #endregion

        #region st test

        public st_test ReadStTest()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_test_name);
            var _st_test = new st_test();
            int datastream_count = 19 + 10 * 20 + 11/*241016*/;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_test.use_ch = new bool[10];
            _st_test._out = new stsub_testdisplay[10];
            _st_test.TestConditions = new bool[10];
            _st_test.test_busy = binread.ReadBoolean();
            _st_test.butt_test_start = binread.ReadBoolean();
            _st_test.butt_test_stop = binread.ReadBoolean();
            _st_test.butt_test_guandao = binread.ReadBoolean();
            _st_test.butt_test_moqiang = binread.ReadBoolean();
            _st_test.test_time = binread.ReadInt16();
            _st_test.holding_time = binread.ReadInt16();

            for (int i = 0; i < 10; i++)
            {
                _st_test.use_ch[i] = binread.ReadBoolean();
            }

            for (int i = 0; i < 10; i++)
            {
                _st_test._out[i].Fr_P = binread.ReadSingle();
                _st_test._out[i].Se_P = binread.ReadSingle();
                _st_test._out[i].close_time = binread.ReadSingle();
                _st_test._out[i].deltaP = binread.ReadSingle();
                _st_test._out[i].sulv_P = binread.ReadSingle();
            }
            _st_test.Test_Conditions = binread.ReadBoolean();
            for (int i = 0; i < 10; i++)
            {
                _st_test.TestConditions[i] = binread.ReadBoolean();
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_test;
        }
        #endregion
        #region auto
        public st_hmiauto Readst_hmiauto()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_auto_name);
            var _st_hmiauto = new st_hmiauto();
            //int datastream_count = 11 + 6 * (plcD_count + plcC_count + plcE_count);
            // Anders 20250219 数组长度6-8，新增dis_HYDTest变量
            int datastream_count = 11 + 6 * (plcD_count + plcC_count + plcE_count);
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_hmiauto.condition = new bool[6];
            _st_hmiauto.led_D = new st_ledauto[plcD_count];
            _st_hmiauto.led_C = new st_ledauto[plcC_count];
            _st_hmiauto.led_E = new st_ledauto[plcE_count];
            _st_hmiauto.auto_condition = binread.ReadBoolean();
            //_st_hmiauto.dis_HYDTest = binread.ReadBoolean();
            for (int i = 0; i < 6; i++)
            {
                _st_hmiauto.condition[i] = binread.ReadBoolean();
            }
            _st_hmiauto.status_autoruning = binread.ReadBoolean();
            _st_hmiauto.butt_autostart = binread.ReadBoolean();
            _st_hmiauto.butt_autostop = binread.ReadBoolean();
            _st_hmiauto.butt_useVac_hotmould = binread.ReadBoolean();

            for (int i = 0; i < plcD_count; i++)
            {
                _st_hmiauto.led_D[i].led_course = new bool[6];
                for (int j = 0; j < 6; j++)
                {
                    _st_hmiauto.led_D[i].led_course[j] = binread.ReadBoolean();
                }
            }
            for (int i = 0; i < plcC_count; i++)
            {
                _st_hmiauto.led_C[i].led_course = new bool[6];
                for (int j = 0; j < 6; j++)
                {
                    _st_hmiauto.led_C[i].led_course[j] = binread.ReadBoolean();
                }
            }
            for (int i = 0; i < plcE_count; i++)
            {
                _st_hmiauto.led_E[i].led_course = new bool[6];
                for (int j = 0; j < 6; j++)
                {
                    _st_hmiauto.led_E[i].led_course[j] = binread.ReadBoolean();
                }
            }


            adsClient.DeleteVariableHandle(hvar);
            return _st_hmiauto;
        }
        #endregion
        #region 真空源
        public st_VacSource Readst_VacSource()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_Vacsource_name);
            var _st_VacSource = new st_VacSource();
            int datastream_count = 1 * 38 + 4 * 6;
            //int datastream_count = 1 * 38 + 4 * 6 + 10;//东风
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_VacSource.status_pump = new bool[3];
            _st_VacSource.current_pump = new float[3];
            _st_VacSource.PG_tank = new float[3];
            _st_VacSource.led_pumps_v = new bool[6];
            _st_VacSource.led_VY1_pump = new bool[3];
            _st_VacSource.led_V2_pump = new bool[3];
            _st_VacSource.led_tank1_v = new bool[3];

            _st_VacSource.hmi_butt_VY1_pump = new bool[3];
            _st_VacSource.hmi_butt_V2_pump = new bool[3];
            _st_VacSource.hmi_butt_pumps_V = new bool[6];
            _st_VacSource.hmi_butt_tank1_v = new bool[2];

            //_st_VacSource.led_ExTank_v = new bool[5];
            //_st_VacSource.butt_ExTank_v = new bool[5];


            _st_VacSource.butt_pump_start = binread.ReadBoolean();
            _st_VacSource.butt_pump_stop = binread.ReadBoolean();
            _st_VacSource.status_runing = binread.ReadBoolean();
            _st_VacSource.busy_reveal_C = binread.ReadBoolean();
            _st_VacSource.busy_reveal_E = binread.ReadBoolean();
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.status_pump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.current_pump[i] = binread.ReadSingle();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.PG_tank[i] = binread.ReadSingle();
            }
            for (int i = 0; i < 6; i++)
            {
                _st_VacSource.led_pumps_v[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.led_VY1_pump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.led_V2_pump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_VacSource.led_tank1_v[i] = binread.ReadBoolean();
            }
            _st_VacSource.led_tank2_v1 = binread.ReadBoolean();

            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.hmi_butt_VY1_pump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_VacSource.hmi_butt_V2_pump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 6; i++)
            {
                _st_VacSource.hmi_butt_pumps_V[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_VacSource.hmi_butt_tank1_v[i] = binread.ReadBoolean();
            }
            _st_VacSource.hmi_butt_tank2_v1 = binread.ReadBoolean();
            //for (int i = 0;  i < 5;  i++)
            //{
            //    _st_VacSource.led_ExTank_v[i] = binread.ReadBoolean();
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    _st_VacSource.butt_ExTank_v[i] = binread.ReadBoolean();
            //}
            adsClient.DeleteVariableHandle(hvar);
            return _st_VacSource;
        }

        public st_ups Readst_ups()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(st_ups_name);
            var _st_ups = new st_ups();
            //int datastream_count = 26 + 41 + 12 + 30 + 60 + 72 + 12/*241008*/+ 4 + 3 + 4 * 4 + 2 * 9 + 6 + 4 * 2 + 1 + 4 * 7 + 2;
            //int datastream_count = 26 + 41 + 12 + 30 + 60 + 73 + 12/*241008*/+ 4 + 3 + 4 * 4 + 2 * 9 + 6 + 4 * 2 + 1 + 4 * 7 + 2;
            int datastream_count = 26 + 41 + 12 + 30 + 60 + 72 + 12/*241008*/+ 4 + 3 + 4 * 4 + 2 * 9 + 6 + 4 * 2 + 1 + 4 * 7 + 2 + 1 + 8;
            //int datastream_count = 26 + 41 + 12 + 30 + 60 + 72 + 12/*241008*/+ 4 + 3 + 4 * 4 + 2 * 9 + 6 + 4 * 2 + 1 + 4 * 7 + 2 + 1 + 8 + 2;

            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_ups.runtime_pump = new float[3];
            _st_ups.butt_i_dis = new bool[3];
            _st_ups.set_pump_upkeepTime = new float[3];
            _st_ups.set_Amps = new float[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.runtime_pump[i] = binread.ReadSingle();
            }
            _st_ups.pump_mode1 = binread.ReadBoolean();
            _st_ups.pump_mode2 = binread.ReadBoolean();
            _st_ups.pump_mode3 = binread.ReadBoolean();
            _st_ups.use_standbyV6 = binread.ReadBoolean();
            _st_ups.Vacsource_D = binread.ReadInt16();
            _st_ups.Vacsource_C = binread.ReadInt16();
            _st_ups.Vacsource_E = binread.ReadInt16();
            _st_ups.set_pump_C = binread.ReadInt16();
            _st_ups.set_pump_E = binread.ReadInt16();//26
            _st_ups.Countdown_pump = new float[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Countdown_pump[i] = binread.ReadSingle();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_ups.butt_i_dis[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                _st_ups.set_pump_upkeepTime[i] = binread.ReadSingle();
            }
            _st_ups.use_CycleTime = binread.ReadBoolean();
            _st_ups.set_CycleTime_value = binread.ReadSingle();
            _st_ups.use_TestCoolingWather = binread.ReadBoolean();
            _st_ups.use_TestCompressedAir = binread.ReadBoolean();
            _st_ups.set_CompressedAir_lo = binread.ReadSingle();
            _st_ups.use_atandby_vavle = binread.ReadBoolean();
            _st_ups.autoUse_PumpRuningCondition = binread.ReadBoolean();
            _st_ups.hmi_Butt_BOV = binread.ReadBoolean();

            //k3
            _st_ups.K_pos = binread.ReadSingle();
            _st_ups.offset_pos = binread.ReadSingle();
            _st_ups.Safety_value_pos = binread.ReadInt16();
            _st_ups.Safety_time = binread.ReadInt16();
            //k5
            _st_ups.Use_Upkeep_pump = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Use_Upkeep_pump[i] = binread.ReadBoolean();
            }
            _st_ups.Use_AirFilter_pump = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Use_AirFilter_pump[i] = binread.ReadBoolean();
            }
            _st_ups.Use_PaperFilter_D = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Use_PaperFilter_D[i] = binread.ReadBoolean();
            }
            _st_ups.Use_IronFilter_D = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Use_IronFilter_D[i] = binread.ReadBoolean();
            }
            _st_ups.Use_PaperFilter_C = binread.ReadBoolean();
            _st_ups.Use_IronFilter_C = binread.ReadBoolean();
            _st_ups.Use_PaperFilter_E = binread.ReadBoolean();
            _st_ups.Use_IronFilter_E = binread.ReadBoolean();

            _st_ups.Time_AirFilter_pump = new float[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Time_AirFilter_pump[i] = binread.ReadSingle();
            }
            _st_ups.Time_PaperFilter_D = new ushort[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Time_PaperFilter_D[i] = binread.ReadUInt16();
            }
            _st_ups.Time_IronFilter_D = new ushort[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Time_IronFilter_D[i] = binread.ReadUInt16();
            }
            _st_ups.Time_PaperFilter_C = binread.ReadUInt16();
            _st_ups.Time_IronFilter_C = binread.ReadUInt16();
            _st_ups.Time_PaperFilter_E = binread.ReadUInt16();
            _st_ups.Time_IronFilter_E = binread.ReadUInt16();

            _st_ups.Upkeep_pump = new float[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Upkeep_pump[i] = binread.ReadSingle();
            }
            _st_ups.Upkeep_AirFilter_pump = new float[3];
            for (int i = 0; i < 3; i++)
            {
                _st_ups.Upkeep_AirFilter_pump[i] = binread.ReadSingle();
            }
            _st_ups.Upkeep_PaperFilter_D = new ushort[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Upkeep_PaperFilter_D[i] = binread.ReadUInt16();
            }
            _st_ups.Upkeep_IronFilter_D = new ushort[10];
            for (int i = 0; i < 10; i++)
            {
                _st_ups.Upkeep_IronFilter_D[i] = binread.ReadUInt16();
            }
            _st_ups.Upkeep_PaperFilter_C = binread.ReadUInt16();
            _st_ups.Upkeep_IronFilter_C = binread.ReadUInt16();
            _st_ups.Upkeep_PaperFilter_E = binread.ReadUInt16();
            _st_ups.Upkeep_IronFilter_E = binread.ReadUInt16();
            //K6
            //_st_ups.Use_HYD = new bool[2];//Anders 20250219 
            _st_ups.Use_HYD1 = binread.ReadBoolean();
            //_st_ups.Use_HYD[1] = binread.ReadBoolean();
            _st_ups.Ust_timeControl_HYD = binread.ReadBoolean();
            _st_ups.set_HYD_time = binread.ReadInt16();
            _st_ups.set_Hyd_hi = binread.ReadSingle();
            _st_ups.set_Hyd_lo = binread.ReadSingle();
            //(*新增241017*)
            _st_ups.set_Upkeep_HYD = binread.ReadSingle();
            //(*******新增241008 * **********)
            _st_ups.use_DCMcloseTest = binread.ReadBoolean();
            _st_ups.use_SpoolTest = binread.ReadBoolean();
            _st_ups.use_openRise = binread.ReadBoolean();

            _st_ups.DCMcloseTest_hi = binread.ReadSingle();/**/
            _st_ups.DCMcloseTest_lo = binread.ReadSingle();
            _st_ups.SpoolTest_hihi = binread.ReadSingle();
            _st_ups.SpoolTest_hi = binread.ReadSingle();//12

            _st_ups.Spool_ProbeTime = binread.ReadInt16();
            _st_ups.Spool_testTime = binread.ReadInt16();
            _st_ups.ProbeTime = binread.ReadInt16();
            _st_ups.testTime = binread.ReadInt16();

            _st_ups.VacLatency = binread.ReadInt16();
            _st_ups.use_Vacsign = binread.ReadInt16();
            _st_ups.use_VacTime = binread.ReadInt16();

            _st_ups.SafetyVel = binread.ReadInt16();
            _st_ups.SafetyTime = binread.ReadInt16();
            _st_ups.use_P = binread.ReadBoolean();
            _st_ups.Use_MBlow = binread.ReadBoolean();
            _st_ups.use_autoEndS = binread.ReadBoolean();
            _st_ups.select_gauging_M = binread.ReadBoolean();

            _st_ups.use_TakePos = binread.ReadBoolean();

            _st_ups.setUse_standbyValve = binread.ReadBoolean();

            _st_ups.Set_tank_StartPG = binread.ReadSingle();
            _st_ups.Set_RootsPump_StartPG = binread.ReadSingle();

            _st_ups.use_tankSET = binread.ReadBoolean();

            _st_ups.Set_tankHI = binread.ReadSingle();
            _st_ups.Set_tankLO = binread.ReadSingle();
            _st_ups.Set_tank1HI = binread.ReadSingle();
            _st_ups.Set_tank1LO = binread.ReadSingle();

            for (int i = 0; i < 3; i++)
            {
                _st_ups.set_Amps[i] = binread.ReadSingle();
            }
            _st_ups.set_kaidu = binread.ReadInt16();

            _st_ups.Use_HYD2 = binread.ReadBoolean();//20250220

            _st_ups.HYD_TestHI = binread.ReadSingle();
            _st_ups.HYD_TestLo = binread.ReadSingle();

            // anders 20250318 
            //_st_ups.E_setsource = binread.ReadBoolean();
            //_st_ups.C_setsource = binread.ReadBoolean();

            adsClient.DeleteVariableHandle(hvar);
            return _st_ups;
        }
        #endregion
        #region YSG
        //public st_ysgjz Readst_ysgjz()
        //{
        //    CheckAdsConnection();
        //    int hvar = new int();
        //    hvar = adsClient.CreateVariableHandle(st_ysgjz_name);
        //    var _st_ysgjz = new st_ysgjz();
        //    int datastream_count = 13;
        //    AdsStream datastream = new AdsStream(datastream_count);
        //    BinaryReader binread = new BinaryReader(datastream);
        //    datastream.Position = 0;
        //    adsClient.Read(hvar, datastream);
        //    _st_ysgjz.K_pos = binread.ReadSingle();
        //    _st_ysgjz.offset_pos = binread.ReadSingle();
        //    _st_ysgjz.Safety_value_pos = binread.ReadInt16();
        //    _st_ysgjz.Safety_time = binread.ReadInt16();
        //    _st_ysgjz.hmi_butt_save = binread.ReadBoolean();
        //    adsClient.DeleteVariableHandle(hvar);
        //    return _st_ysgjz;
        //}
        #endregion
        #region IO
        public st_hmi_IO Readst_hmi_IO()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_IO_name);
            var _st_hmi_IO = new st_hmi_IO();
            int datastream_count = 9 * 8 + 2 * 2 * 8 + 4 * 5 + GlobalVar.systemSetting.e_chn_count * 12 + GlobalVar.systemSetting.c_chn_count * 20 + GlobalVar.systemSetting.d_chn_count * 28;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_hmi_IO.hmi_P1_DI1 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI1[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI2 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI2[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI3 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI3[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI4 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI4[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI5 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI5[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI6 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI6[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DI7 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DI7[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_AI1 = new short[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_AI1[i] = binread.ReadInt16();
            }
            _st_hmi_IO.hmi_P1_AI2 = new short[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_AI2[i] = binread.ReadInt16();
            }
            _st_hmi_IO.hmi_P1_DO1 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DO1[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO2 = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                _st_hmi_IO.hmi_P1_DO2[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO3 = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                _st_hmi_IO.hmi_P1_DO3[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO4 = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                _st_hmi_IO.hmi_P1_DO4[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO5 = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                _st_hmi_IO.hmi_P1_DO5[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO6 = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                _st_hmi_IO.hmi_P1_DO6[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_P1_DO7 = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                _st_hmi_IO.hmi_P1_DO7[i] = binread.ReadBoolean();
            }
            _st_hmi_IO.hmi_E = new st_IO_E[GlobalVar.systemSetting.e_chn_count];
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                _st_hmi_IO.hmi_E[i] = new st_IO_E();
                _st_hmi_IO.hmi_E[i].AI1 = new short[4];
                _st_hmi_IO.hmi_E[i].DO1 = new bool[4];
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_E[i].AI1[j] = binread.ReadInt16();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_E[i].DO1[j] = binread.ReadBoolean();
                }
            }

            _st_hmi_IO.hmi_C = new st_IO_C[GlobalVar.systemSetting.c_chn_count];
            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                _st_hmi_IO.hmi_C[i] = new st_IO_C();
                _st_hmi_IO.hmi_C[i].AI1 = new short[4];
                _st_hmi_IO.hmi_C[i].DO1 = new bool[4];
                _st_hmi_IO.hmi_C[i].DO2 = new bool[4];
                _st_hmi_IO.hmi_C[i].DO3 = new bool[4];
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_C[i].AI1[j] = binread.ReadInt16();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_C[i].DO1[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_C[i].DO2[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_C[i].DO3[j] = binread.ReadBoolean();
                }
            }

            _st_hmi_IO.hmi_D = new st_IO_D[GlobalVar.systemSetting.d_chn_count];
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                _st_hmi_IO.hmi_D[i] = new st_IO_D();
                _st_hmi_IO.hmi_D[i].DI1 = new bool[8];
                _st_hmi_IO.hmi_D[i].AI1 = new short[4];
                _st_hmi_IO.hmi_D[i].DO1 = new bool[4];
                _st_hmi_IO.hmi_D[i].DO2 = new bool[4];
                _st_hmi_IO.hmi_D[i].DO3 = new bool[4];
                for (int j = 0; j < 8; j++)
                {
                    _st_hmi_IO.hmi_D[i].DI1[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_D[i].AI1[j] = binread.ReadInt16();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_D[i].DO1[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_D[i].DO2[j] = binread.ReadBoolean();
                }
                for (int j = 0; j < 4; j++)
                {
                    _st_hmi_IO.hmi_D[i].DO3[j] = binread.ReadBoolean();
                }
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_hmi_IO;
        }
        #endregion

        #region 设备管理
        public st_Ksystem Readst_Ksystem()
        {
            CheckAdsConnection();
            int hvar = new int();
            hvar = adsClient.CreateVariableHandle(hmi_K_name);
            var _st_Ksystem = new st_Ksystem();
            int datastream_count = 9 + 30 + 18 + 12 + 2 + 4 * 6;
            AdsStream datastream = new AdsStream(datastream_count);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;
            adsClient.Read(hvar, datastream);
            _st_Ksystem.P_CompressedAir = binread.ReadSingle();
            _st_Ksystem.auto_CycleTime = binread.ReadSingle();
            _st_Ksystem.Led_CoolingWather = binread.ReadBoolean();

            _st_Ksystem.ButtCL_PumpUpkeep = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                _st_Ksystem.ButtCL_PumpUpkeep[i] = binread.ReadBoolean();
            }

            _st_Ksystem.ButtCL_AirFilter_pump = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                _st_Ksystem.ButtCL_AirFilter_pump[i] = binread.ReadBoolean();
            }

            _st_Ksystem.ButtCL_PaperFilter_D = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                _st_Ksystem.ButtCL_PaperFilter_D[i] = binread.ReadBoolean();
            }

            _st_Ksystem.ButtCL_IronFilter_D = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                _st_Ksystem.ButtCL_IronFilter_D[i] = binread.ReadBoolean();
            }
            _st_Ksystem.ButtCL_PaperFilter_C = binread.ReadBoolean();
            _st_Ksystem.ButtCL_IronFilter_C = binread.ReadBoolean();
            _st_Ksystem.ButtCL_PaperFilter_E = binread.ReadBoolean();
            _st_Ksystem.ButtCL_IronFilter_E = binread.ReadBoolean();

            _st_Ksystem.Pt_Hyd = new float[2];
            _st_Ksystem.butt_CL_HYDpump = new bool[2];
            _st_Ksystem.countdown_HYDpump = new float[2];
            _st_Ksystem.HYDpump_runtime = new float[2];
            _st_Ksystem.HYDpump_timing = new float[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.Pt_Hyd[i] = binread.ReadSingle();
            }
            _st_Ksystem.T_Hyd = new float[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.T_Hyd[i] = binread.ReadSingle();
            }
            _st_Ksystem.Alarm_Hyd = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.Alarm_Hyd[i] = binread.ReadBoolean();
            }

            _st_Ksystem.led_remote = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.led_remote[i] = binread.ReadBoolean();
            }
            _st_Ksystem.Led_hydRuning = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.Led_hydRuning[i] = binread.ReadBoolean();
            }
            _st_Ksystem.Led_valve = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.Led_valve[i] = binread.ReadBoolean();
            }
            _st_Ksystem.butt_start_hyd = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.butt_start_hyd[i] = binread.ReadBoolean();
            }
            _st_Ksystem.butt_stop_hyd = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.butt_stop_hyd[i] = binread.ReadBoolean();
            }
            _st_Ksystem.open_Valve = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.open_Valve[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.butt_CL_HYDpump[i] = binread.ReadBoolean();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.countdown_HYDpump[i] = binread.ReadSingle();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.HYDpump_runtime[i] = binread.ReadSingle();
            }
            for (int i = 0; i < 2; i++)
            {
                _st_Ksystem.HYDpump_timing[i] = binread.ReadSingle();
            }
            adsClient.DeleteVariableHandle(hvar);
            return _st_Ksystem;
        }

        #endregion
    }
}
