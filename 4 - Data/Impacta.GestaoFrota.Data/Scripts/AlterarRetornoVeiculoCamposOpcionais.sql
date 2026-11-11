BEGIN;

ALTER TABLE frota.retorno_veiculo
	ALTER COLUMN id_local_retorno DROP NOT NULL,
	ALTER COLUMN id_status DROP NOT NULL;

COMMIT;
