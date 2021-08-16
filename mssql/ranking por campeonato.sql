SELECT
DISTINCT(P.IdUsuario),
U.Apelido ApelidoUsuario,
U.NomeImagemAvatar NomeImagemAvatarUsuario,
SUM(P.Pontos) Pontos,
COUNT(P.Id) QuantidadePalpites
FROM
PALPITE P,
USUARIO U,
CAMPEONATO C,
JOGO J
WHERE 
P.IdUsuario = U.Id AND
P.IdJogo = J.Id AND
J.IdCampeonato = C.Id AND
J.Finalizado = 1 AND
P.Finalizado = 1 AND
C.Id = 1
GROUP by p.IdUsuario, u.Apelido, u.NomeImagemAvatar
ORDER BY Pontos DESC, QuantidadePalpites ASC