-- Script alternativo para popular nomes aleatórios de veículos baseado no fabricante
-- Este script usa UPDATE simples sem procedimentos para melhor compatibilidade

BEGIN TRANSACTION;

-- Atualiza Toyotas
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 7)
	WHEN 0 THEN 'Corolla #' || id_veiculo
	WHEN 1 THEN 'Hilux #' || id_veiculo
	WHEN 2 THEN 'Yaris #' || id_veiculo
	WHEN 3 THEN 'Etios #' || id_veiculo
	WHEN 4 THEN 'Camry #' || id_veiculo
	WHEN 5 THEN 'RAV4 #' || id_veiculo
	ELSE 'Prius #' || id_veiculo
END
WHERE fabricante ILIKE '%Toyota%' AND (nome IS NULL OR nome = '');

-- Atualiza Fords
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 7)
	WHEN 0 THEN 'Fiesta #' || id_veiculo
	WHEN 1 THEN 'Focus #' || id_veiculo
	WHEN 2 THEN 'Fusion #' || id_veiculo
	WHEN 3 THEN 'Ranger #' || id_veiculo
	WHEN 4 THEN 'EcoSport #' || id_veiculo
	WHEN 5 THEN 'Territory #' || id_veiculo
	ELSE 'Mustang #' || id_veiculo
END
WHERE fabricante ILIKE '%Ford%' AND (nome IS NULL OR nome = '');

-- Atualiza Fiats
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 7)
	WHEN 0 THEN 'Uno #' || id_veiculo
	WHEN 1 THEN 'Palio #' || id_veiculo
	WHEN 2 THEN 'Siena #' || id_veiculo
	WHEN 3 THEN 'Argo #' || id_veiculo
	WHEN 4 THEN 'Cronos #' || id_veiculo
	WHEN 5 THEN 'Strada #' || id_veiculo
	ELSE 'Toro #' || id_veiculo
END
WHERE fabricante ILIKE '%Fiat%' AND (nome IS NULL OR nome = '');

-- Atualiza Chevrolets
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 7)
	WHEN 0 THEN 'Onix #' || id_veiculo
	WHEN 1 THEN 'Tracker #' || id_veiculo
	WHEN 2 THEN 'Trailblazer #' || id_veiculo
	WHEN 3 THEN 'Cruze #' || id_veiculo
	WHEN 4 THEN 'Spin #' || id_veiculo
	WHEN 5 THEN 'Colorado #' || id_veiculo
	ELSE 'S10 #' || id_veiculo
END
WHERE fabricante ILIKE '%Chevrolet%' AND (nome IS NULL OR nome = '');

-- Atualiza Volkswagens
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 7)
	WHEN 0 THEN 'Golf #' || id_veiculo
	WHEN 1 THEN 'Polo #' || id_veiculo
	WHEN 2 THEN 'Gol #' || id_veiculo
	WHEN 3 THEN 'Fox #' || id_veiculo
	WHEN 4 THEN 'Voyage #' || id_veiculo
	WHEN 5 THEN 'Passat #' || id_veiculo
	ELSE 'Tiguan #' || id_veiculo
END
WHERE (fabricante ILIKE '%Volkswagen%' OR fabricante ILIKE '%VW%') AND (nome IS NULL OR nome = '');

-- Atualiza Hyundais
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 6)
	WHEN 0 THEN 'HB20 #' || id_veiculo
	WHEN 1 THEN 'Creta #' || id_veiculo
	WHEN 2 THEN 'Tucson #' || id_veiculo
	WHEN 3 THEN 'Santa Fé #' || id_veiculo
	WHEN 4 THEN 'Elantra #' || id_veiculo
	ELSE 'Veloster #' || id_veiculo
END
WHERE fabricante ILIKE '%Hyundai%' AND (nome IS NULL OR nome = '');

-- Atualiza Hondas
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 6)
	WHEN 0 THEN 'Civic #' || id_veiculo
	WHEN 1 THEN 'Accord #' || id_veiculo
	WHEN 2 THEN 'CR-V #' || id_veiculo
	WHEN 3 THEN 'HR-V #' || id_veiculo
	WHEN 4 THEN 'Fit #' || id_veiculo
	ELSE 'City #' || id_veiculo
END
WHERE fabricante ILIKE '%Honda%' AND (nome IS NULL OR nome = '');

-- Atualiza Jeeps
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 4)
	WHEN 0 THEN 'Renegade #' || id_veiculo
	WHEN 1 THEN 'Compass #' || id_veiculo
	WHEN 2 THEN 'Wrangler #' || id_veiculo
	ELSE 'Cherokee #' || id_veiculo
END
WHERE fabricante ILIKE '%Jeep%' AND (nome IS NULL OR nome = '');

-- Atualiza Renaults
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 5)
	WHEN 0 THEN 'Sandero #' || id_veiculo
	WHEN 1 THEN 'Logan #' || id_veiculo
	WHEN 2 THEN 'Duster #' || id_veiculo
	WHEN 3 THEN 'Captur #' || id_veiculo
	ELSE 'Scenic #' || id_veiculo
END
WHERE fabricante ILIKE '%Renault%' AND (nome IS NULL OR nome = '');

-- Atualiza Peugeots
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 5)
	WHEN 0 THEN '208 #' || id_veiculo
	WHEN 1 THEN '308 #' || id_veiculo
	WHEN 2 THEN '2008 #' || id_veiculo
	WHEN 3 THEN 'Partner #' || id_veiculo
	ELSE '3008 #' || id_veiculo
END
WHERE fabricante ILIKE '%Peugeot%' AND (nome IS NULL OR nome = '');

-- Atualiza registros com fabricantes não mapeados com nomes genéricos
UPDATE frota.veiculos 
SET nome = CASE ((id_veiculo - 1) % 6)
	WHEN 0 THEN 'Veículo A #' || id_veiculo
	WHEN 1 THEN 'Veículo B #' || id_veiculo
	WHEN 2 THEN 'Veículo C #' || id_veiculo
	WHEN 3 THEN 'Veículo D #' || id_veiculo
	WHEN 4 THEN 'Veículo E #' || id_veiculo
	ELSE 'Veículo F #' || id_veiculo
END
WHERE (nome IS NULL OR nome = '');

COMMIT;
