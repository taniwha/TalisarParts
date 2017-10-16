export MODNAME
MODNAME		:= TalisarParts
KSPDIR		:= ${HOME}/ksp/KSP_linux
GAMEDATA	:= ${KSPDIR}/GameData
MODGAMEDATA  := ${GAMEDATA}/TalisarParts

RESGEN2		:= resgen2
GMCS		:= gmcs
GMCSFLAGS	:= -optimize -warnaserror
GIT			:= git
TAR			:= tar
ZIP			:= zip

DATA	:= Talisar_N.png Talisar_S.png

all:

info:
	@echo "${MODNAME} Build Information"
	@echo "    resgen2:    ${RESGEN2}"
	@echo "    gmcs:       ${GMCS}"
	@echo "    gmcs flags: ${GMCSFLAGS}"
	@echo "    git:        ${GIT}"
	@echo "    tar:        ${TAR}"
	@echo "    zip:        ${ZIP}"
	@echo "    KSP Data:   ${KSPDIR}"

clean:

install: all
	mkdir -p ${MODGAMEDATA}/Textures
	cp ${DATA} ${MODGAMEDATA}/Textures

.PHONY: all clean install
