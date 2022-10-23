
SELECT
*
FROM 
CAMPEONATO C
WHERE 
C.Id IN (
    SELECT B.IdCampeonato 
    FROM 
    BOLAO B,
    BOLAO_USUARIO BU
    WHERE 
    B.Id = BU.IdBolao AND
    BU.IdUsuario = @IDUSUARIO
    )