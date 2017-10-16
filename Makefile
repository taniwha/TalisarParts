export MODNAME
export ZIPNAME
MODNAME		:= TalisarParts
ZIPNAME		:= ${MODNAME}
KSPDIR		:= ${HOME}/ksp/KSP_linux
MANAGED		:= ${KSPDIR}/KSP_Data/Managed
GAMEDATA	:= ${KSPDIR}/GameData
MODGAMEDATA  := ${GAMEDATA}/TalisarParts
PLUGINDIR	:= ${MODGAMEDATA}/Plugins

RESGEN2	:= resgen2
GMCS	:= gmcs
GIT		:= git
TAR		:= tar
ZIP		:= zip

.PHONY: all clean info

SUBDIRS=Assets GameData Source

DATA	:= \
	README.md \
	$e

all clean:
	@for dir in ${SUBDIRS}; do \
		make -C $$dir $@ || exit 1; \
	done

install:
	@for dir in ${SUBDIRS}; do \
		make -C $$dir $@ || exit 1; \
	done
	mkdir -p ${MODGAMEDATA}
	cp ${DATA} ${MODGAMEDATA}

info:
	@echo "${MODNAME} Build Information"
	@echo "    resgen2:  ${RESGEN2}"
	@echo "    gmcs:     ${GMCS}"
	@echo "    git:      ${GIT}"
	@echo "    tar:      ${TAR}"
	@echo "    zip:      ${ZIP}"
	@echo "    KSP Data: ${KSPDIR}"
	@echo "    Plugin:   ${PLUGINDIR}"

release:
	tools/make-release
