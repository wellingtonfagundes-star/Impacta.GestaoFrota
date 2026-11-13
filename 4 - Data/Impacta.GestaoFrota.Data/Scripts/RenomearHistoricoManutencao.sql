BEGIN;

DO $$
BEGIN
	IF to_regclass('frota.historico') IS NOT NULL THEN
		IF to_regclass('frota.historico_manutencao') IS NOT NULL THEN
			RAISE EXCEPTION 'As tabelas frota.historico e frota.historico_manutencao existem simultaneamente.';
		END IF;

		ALTER TABLE frota.historico RENAME TO historico_manutencao;
	ELSIF to_regclass('frota.historico_manutencao') IS NULL THEN
		RAISE EXCEPTION 'A tabela frota.historico não foi encontrada.';
	END IF;

	IF EXISTS (
		SELECT 1
		FROM information_schema.columns
		WHERE table_schema = 'frota'
		  AND table_name = 'historico_manutencao'
		  AND column_name = 'id_tipoevento'
	) THEN
		IF EXISTS (
			SELECT 1
			FROM information_schema.columns
			WHERE table_schema = 'frota'
			  AND table_name = 'historico_manutencao'
			  AND column_name = 'id_tipo_evento'
		) THEN
			RAISE EXCEPTION 'As colunas id_tipoevento e id_tipo_evento existem simultaneamente.';
		END IF;

		ALTER TABLE frota.historico_manutencao
			RENAME COLUMN id_tipoevento TO id_tipo_evento;
	ELSIF NOT EXISTS (
		SELECT 1
		FROM information_schema.columns
		WHERE table_schema = 'frota'
		  AND table_name = 'historico_manutencao'
		  AND column_name = 'id_tipo_evento'
	) THEN
		RAISE EXCEPTION 'A coluna id_tipoevento não foi encontrada em frota.historico_manutencao.';
	END IF;
END $$;

COMMIT;
