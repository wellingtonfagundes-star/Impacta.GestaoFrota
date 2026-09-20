-- Script para popular nomes aleatórios de veículos baseado no fabricante
-- Executa a atualização de registros que ainda não têm nome

-- Cria uma tabela temporária com nomes por fabricante
DO $$
DECLARE
	v_record RECORD;
	v_nome TEXT;
	v_nomes_toyota TEXT[] := ARRAY['Corolla', 'Hilux', 'Yaris', 'Etios', 'Camry', 'RAV4', 'Prius'];
	v_nomes_ford TEXT[] := ARRAY['Fiesta', 'Focus', 'Fusion', 'Ranger', 'EcoSport', 'Territory', 'Mustang'];
	v_nomes_fiat TEXT[] := ARRAY['Uno', 'Palio', 'Siena', 'Argo', 'Cronos', 'Strada', 'Toro'];
	v_nomes_chevrolet TEXT[] := ARRAY['Onix', 'Tracker', 'Trailblazer', 'Cruze', 'Spin', 'Colorado', 'S10'];
	v_nomes_vw TEXT[] := ARRAY['Golf', 'Polo', 'Gol', 'Fox', 'Voyage', 'Passat', 'Tiguan'];
	v_nomes_hyundai TEXT[] := ARRAY['HB20', 'Creta', 'Tucson', 'Santa Fé', 'Elantra', 'Veloster'];
	v_nomes_honda TEXT[] := ARRAY['Civic', 'Accord', 'CR-V', 'HR-V', 'Fit', 'City'];
	v_nomes_jeep TEXT[] := ARRAY['Renegade', 'Compass', 'Wrangler', 'Cherokee'];
	v_nomes_renault TEXT[] := ARRAY['Sandero', 'Logan', 'Duster', 'Captur', 'Scenic'];
	v_nomes_peugeot TEXT[] := ARRAY['208', '308', '2008', 'Partner', '3008'];
	v_nomes_default TEXT[] := ARRAY['Veículo A', 'Veículo B', 'Veículo C', 'Veículo D', 'Veículo E', 'Veículo F'];
BEGIN
	-- Atualiza Toyotas
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Toyota%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_toyota[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_toyota, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Fords
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Ford%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_ford[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_ford, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Fiats
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Fiat%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_fiat[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_fiat, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Chevrolets
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Chevrolet%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_chevrolet[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_chevrolet, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Volkswagens
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Volkswagen%' OR fabricante ILIKE '%VW%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_vw[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_vw, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Hyundais
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Hyundai%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_hyundai[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_hyundai, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Hondas
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Honda%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_honda[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_honda, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Jeeps
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Jeep%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_jeep[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_jeep, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Renaults
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Renault%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_renault[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_renault, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza Peugeots
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE fabricante ILIKE '%Peugeot%' AND (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_peugeot[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_peugeot, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	-- Atualiza registros com fabricantes não mapeados com nomes genéricos
	FOR v_record IN SELECT id_veiculo FROM frota.veiculos WHERE (nome IS NULL OR nome = '')
	LOOP
		v_nome := v_nomes_default[(FLOOR(RANDOM() * ARRAY_LENGTH(v_nomes_default, 1)))::INT + 1];
		UPDATE frota.veiculos SET nome = v_nome || ' #' || v_record.id_veiculo WHERE id_veiculo = v_record.id_veiculo;
	END LOOP;

	RAISE NOTICE 'Nomes de veículos atualizados com sucesso!';
END $$;
